using System;

namespace TzLookup
{
    public static class TimeZoneLookup
    {
        public static string Iana(float lat, float lon)
        {
            // Make sure lat/lon are valid numbers.
            if (!(lat >= -90 && lat <= 90))
                throw new ArgumentOutOfRangeException(nameof(lat));

            if (!(lon >= -180 && lon <= 180))
                throw new ArgumentOutOfRangeException(nameof(lon));

            // Special case the north pole.
            if (lat >= 90)
                return "Etc/GMT";

            // The tree is essentially a quadtree, but with a very large root node.
            // The root node is 48x24; the width is the smallest such that maritime
            // time zones fall neatly on it's edges (which allows better compression),
            // while the height is chosen such that nodes further down the tree are all
            // square (which is necessary for the properties of a quadtree to hold).

            // Node offset in the tree.
            var n = -1;

            // Location relative to the current node. The root node covers the whole
            // earth (and the tree as a whole is in equirectangular coordinates), so
            // conversion from latitude and longitude is straightforward. The constants
            // are the smallest 64-bit floating-point numbers strictly greater than 360
            // and 180, respectively; we do this so that floor(x)<48 and floor(y)<24.
            // (It introduces a rounding error, but this is negligible.)
            var x = (180 + lon) * 48 / 360.00000000000006;
            var y = (90 - lat) * 24 / 180.00000000000003;

            // Integer coordinates of child node.
            var u = (int)x;
            var v = (int)y;

            // Contents of the child node. The topmost values are reserved for leaf
            // nodes and correspond to the indices of TZLIST. Every other value is a
            // pointer to where the next node in the tree is.
            var i = (v * 96) + (u * 2);
            i = (TZ.DATA[i] * 56) + TZ.DATA[i + 1] - 1995;

            // Recurse until we hit a leaf node.
            while (i + TZ.Names.Length < 3136)
            {
                // Increment the node pointer.
                n = n + i + 1;

                // Find where we are relative to the child node.
                x = (x - u) * 2 % 2;
                y = (y - v) * 2 % 2;
                u = (int)x;
                v = (int)y;

                // Read the child node.
                i = (n * 8) + (v * 4) + (u * 2) + 2304;
                i = (TZ.DATA[i] * 56) + TZ.DATA[i + 1] - 1995;
            }

            // Once we hit a leaf, return the relevant timezone.
            return TZ.Names[i + TZ.Names.Length - 3136];
        }
    }
}