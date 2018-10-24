using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _365H___Tesselations_and_Tilings
{
    class Program
    {
        const string quitString = "\\q";

        static void Main(string[] args)
        {
            int rotationAngle = -1;
            int tileSize = -1;
            int tileTesselation = -1;

            // Get the rotation angle.
            do
            {
                Console.Write("Please enter a rotation angle (multiples of 90 only): ");
                string rotationAngleStr = Console.ReadLine().Trim().ToLower();     // Sanitize the string.
                if (rotationAngleStr == quitString) Environment.Exit(0);            // Quit if quitString is entered.

                if (!int.TryParse(rotationAngleStr, out rotationAngle))
                {
                    Console.WriteLine("Cannot parse rotation angle. Please try again.");
                    rotationAngle = -1;
                }
                else if (rotationAngle % 90 != 0)
                {
                    Console.WriteLine("Invalid rotation angle. Must be a multiple of 90. Please try again.");
                    rotationAngle = -1;
                }

                // Spacing.
                Console.WriteLine();
            } while (rotationAngle % 90 != 0);

            // Get the tile size.
            do
            {
                Console.Write("Please enter a tile size: ");
                string tileSizeStr = Console.ReadLine().Trim().ToLower();     // Sanitize the string.
                if (tileSizeStr == quitString) Environment.Exit(0);           // Quit if quitString is entered.

                if (!int.TryParse(tileSizeStr, out tileSize))
                {
                    Console.WriteLine("Cannot parse tile size. Please try again.");
                    tileSize = -1;
                }
                else if (tileSize == 0)
                {
                    Console.WriteLine("Invalid tile size. Must be greater than 0. Please try again.");
                    tileSize = -1;
                }

                // Spacing.
                Console.WriteLine();
            } while (tileSize < 1);

            // Get the number of tesselations.
            do
            {
                Console.Write("Please enter the number of times to tesselate the tile: ");
                string tileTesselationStr = Console.ReadLine().Trim().ToLower();     // Sanitize the string.
                if (tileTesselationStr == quitString) Environment.Exit(0);           // Quit if quitString is entered.

                if (!int.TryParse(tileTesselationStr, out tileTesselation))
                {
                    Console.WriteLine("Cannot parse tile tesselation. Please try again.");
                    tileTesselation = -1;
                }
                else if (tileTesselation == 0)
                {
                    Console.WriteLine("Invalid tile tesselation. Must be greater than 0. Please try again.");
                    tileTesselation = -1;
                }

                // Spacing.
                Console.WriteLine();
            } while (tileTesselation < 1);


            // Get the actual tile contents.
            char[,] tile = new char[tileSize, tileSize];
            Console.WriteLine(string.Format("\nPlease input tile of size {0} below:", tileSize));
            for (int i = 0; i < tileSize;)
            {
                string tileLine = Console.ReadLine().Trim().ToLower();     // Sanitize the string.

                // If the user submits an improperly-sized line, have them reenter that line.
                if (tileLine.Length != tileSize)
                {
                    Console.WriteLine("Last line was of invalid length. Please reenter:");
                }
                else
                {
                    for (int j = 0; j < tileSize; j++)
                    {
                        tile[i, j] = tileLine[j];
                    }

                    // Increment the line we are fetching once we have received a valid line.
                    i++;
                }
            }

            // Spacing.
            Console.WriteLine("\nTesselated tile:");

            // Generate and dump tesselated tile.
            List<string> tileTesselationStrings = TesselateTile(tile, tileTesselation, rotationAngle);
            foreach (string s in tileTesselationStrings) Console.WriteLine(s);

            Console.WriteLine("\nPress enter to exit.");
            Console.Read();
        }

        // Given a 2d char array tile and a rotation angle (which must be a multiple of 90), this method will return a 2d char array containing the tile rotated to the specified angle.
        private static char[,] RotateTile(char[,] tile, int rotation)
        {
            if (tile.GetLength(0) != tile.GetLength(1)) throw new ArgumentException("tile must be a 2d array with height being equal to width.", "tile");
            if (tile.GetLength(0) == 0) throw new ArgumentException("tile cannot be an empty 2d array.", "tile");
            if (rotation % 90 != 0) throw new ArgumentException("rotation must be a multiple of 90.", "rotation");

            // Any 2d array of size 1 being rotated around any angle will simply result in the same tile, so just kick out immediately if that's the case.
            if (tile.GetLength(0) == 1) return tile;

            // Make sure that rotation is between the bounds of [0, 270].
            while (rotation < 0)
            {
                rotation += 360;
            }
            while (rotation >= 360)
            {
                rotation -= 360;
            }

            // If the rotation angle is 0, then return the original tile.
            if (rotation == 0) return tile;

            int tileSize = tile.GetLength(0);
            char[,] newTile = new char[tileSize, tileSize];
            int tileMax = tileSize - 1; // Short hand for keeping track of the 0-based tile size.

            for (int x = 0; x < tileSize; x++)
            {
                for (int y = 0; y < tileSize; y++)
                {
                    switch (rotation)
                    {
                        case 90:
                            newTile[x, y] = tile[tileMax - y, x];
                            break;
                        case 180:
                            newTile[x, y] = tile[tileMax - x, tileMax - y];
                            break;
                        case 270:
                            newTile[x, y] = tile[y, tileMax - x];
                            break;
                        default:
                            throw new Exception("rotation out of bounds!");
                    }
                }
            }

            return newTile;
        }

        // Given a 2d char array tile and an integer indicating how many times to tesselate it in either direction, this method will return a list of strings which represent the tesselated tile.
        private static List<string> TesselateTile(char[,] tile, int tesselationWidthInTiles, int rotationAnglePerTesselation)
        {
            if (tile.GetLength(0) != tile.GetLength(1)) throw new ArgumentException("tile must be a 2d array with height being equal to width.", "tile");
            if (tile.GetLength(0) == 0) throw new ArgumentException("tile cannot be an empty 2d array.", "tile");
            if (tesselationWidthInTiles < 1) throw new ArgumentOutOfRangeException("tesselationWidthInTiles", "tesselationWidthInTiles must be at least 1.");
            if (rotationAnglePerTesselation % 90 != 0) throw new ArgumentException("rotationAnglePerTesselation must be a multiple of 90.", "rotationAnglePerTesselation");

            List<List<char[,]>> tileRows = new List<List<char[,]>>();

            for (int row=0; row < tesselationWidthInTiles; row++)
            {
                List<char[,]> currentTileRow = new List<char[,]>();
                for (int col=0; col < tesselationWidthInTiles; col++)
                {
                    // According to the problem description, it can be determined that the location of a tile affects its rotation.
                    // Every additional column and row indicates an additional rotation around the rotation angle.
                    // So we can find the number of rotations by adding the row and column and getting the remainder after dividing by 4.
                    int timesToRotate = (row + col) % 4;
                    currentTileRow.Add(RotateTile(tile, rotationAnglePerTesselation * timesToRotate));
                }
                tileRows.Add(currentTileRow);
            }

            // Create a list to contain the final string representation.
            List<string> resultingStrings = new List<string>();

            // Combine all tile rows and add them to the resultingStrings list.
            foreach (var row in tileRows)
            {
                resultingStrings.AddRange(CombineTileRow(row));
            }

            return resultingStrings;
        }

        // Given a list of tiles (2d char arrays), generates the list of strings that would be created by laying the tiles side-by-side.
        private static List<string> CombineTileRow(List<char[,]> tileRow)
        {
            if (tileRow.Count < 1) throw new ArgumentException("tileRow cannot be empty.", "tileRow");

            int tileSize = tileRow[0].GetLength(0);
            if (tileSize < 1) throw new ArgumentException("tiles in tileRow must not be empty.", "tileRow");

            List<string> resultingStrings = new List<string>();

            // Go through all tiles row by row.
            for (int y = 0; y < tileSize; y++)
            {
                // Iterate through the current row in each tile, and append the contents to the rowBuilder.
                StringBuilder rowBuilder = new StringBuilder();
                foreach (var tile in tileRow)
                {
                    for (int x = 0; x < tileSize; x++)
                    {
                        rowBuilder.Append(tile[y, x]);
                    }
                }

                // Store each completed row string.
                resultingStrings.Add(rowBuilder.ToString());
            }

            return resultingStrings;
        }
    }
}
