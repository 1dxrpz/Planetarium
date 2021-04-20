using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PerlinNoise;

namespace Planetarium.Engine.Utils.Noise.PerlinNoise.Filters
{
	class PixellatedColorFilter
	{
		int PixelSize = 10;
		public struct ColorPair
		{
			public float Start;
			public float End;
			public Color Color;

			public ColorPair(float start, float end, Color color)
			{
				this.Start = start;
				this.End = end;
				this.Color = color;
			}

			public bool InRange(float val)
			{
				return val < End && val >= Start;
			}

			public override string ToString()
			{
				return Color + "[" + Start + ", " + End + ")";
			}
		}
		public List<ColorPair> Colors { get; private set; }
		public PixellatedColorFilter(int size)
		{
			PixelSize = size;
			Colors = new List<ColorPair>();
		}
		public NoiseField<Color> Filter(NoiseField<float> field)
		{
			NoiseField<Color> result = new NoiseField<Color>(field.Width, field.Height);
			//Colors = Colors.Where((x, i) => i % PixelSize == 0).ToList();

			for (int x = 0; x < field.Width / PixelSize; x++)
			{
				for (int y = 0; y < field.Height / PixelSize; y++)
				{
					float fieldValue = field.Field[x * PixelSize, y * PixelSize];

					foreach (var pair in Colors)
					{
						for (int a = 0; a < PixelSize; a++)
						{
							for (int b = 0; b < PixelSize; b++)
							{
								if (pair.InRange(fieldValue))
								{
									result.Field[x * PixelSize + a, y * PixelSize + b] =
										new Color((int)(pair.Color.R),
												  (int)(pair.Color.G),
												  (int)(pair.Color.B)
										);
								}
							}
						}
					}
				}
			}

			return result;
		}
		public void AddColorPoint(float start, float end, Color color)
		{
			ColorPair pair = new ColorPair(start, end, color);

			Colors.Add(pair);
		}
	}
}
