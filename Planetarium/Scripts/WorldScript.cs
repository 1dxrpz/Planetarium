using System;
using System.IO;
using System.Threading.Tasks;
using GameEngineTK.Engine;
using GameEngineTK.Engine.Components;
using GameEngineTK.Engine.Prototypes.Interfaces;
using GameEngineTK.Scripts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PerlinNoise;
using PerlinNoise.Filters;
using PerlinNoise.Transformers;
using Planetarium.Engine.Utils.Noise.PerlinNoise.Filters;
using SharpNoise;
using SharpNoise.Modules;

namespace Planetarium.Scripts
{
	class WorldScript : IScriptManager
	{
		NoiseField<float> perlinNoise;
		
		Perlin perlin;
		public Texture2D GenerateNoiseTexture(int seed, int a, int b)
		{
			Texture2D noiseTexture = new Texture2D(ScriptManager.graphicsDevice, 100, 100);
			perlin = new Perlin();
			perlin.OctaveCount = 2;
			perlin.Persistence = 10;
			perlin.Quality = NoiseQuality.Standard;
			perlin.Seed = seed;

			Color[] data = new Color[100 * 100];

			for (int i = 0; i < 100; i++)
			{
				for (int n = 0; n < 100; n++)
				{
					var color = perlin.GetValue((double)n / 100 + a, (double)i / 100 + b, 0) > .1 ? Color.Gray : Color.White;
					data[i * 100 + n] = color;
				}
			}
			
			noiseTexture.SetData(data);
			return noiseTexture;
		}
		GameObject[] noise;
		public void Start()
		{
			GenerateNoiseTexture(1, 0, 0);

			noise = new GameObject[9]
			{
				new GameObject(),
				new GameObject(),
				new GameObject(),
				new GameObject(),
				new GameObject(),
				new GameObject(),
				new GameObject(),
				new GameObject(),
				new GameObject()
			};

			foreach (var item in noise)
			{
				item.GetComponent<Transform>().Width = 100;
				item.GetComponent<Transform>().Height = 100;
				item.GetComponent<Transform>().Position = new Vector2(0, 0);
				item.AddComponent(new Sprite());
				item.GetComponent<Sprite>().Texture = GenerateNoiseTexture(1, 0, 0);
			}

		}

		bool lastChunk = false;
		int prevx = 0;
		int prevy = 0;
		int sizex = 100;
		int sizey = 100;
		public void Update()
		{
			Transform pt = PlayerScript.Player.GetComponent<Transform>();
			if (prevx != (int)Math.Floor(pt.Position.X / sizex) * sizex ||
				prevy != (int)Math.Floor(pt.Position.Y / sizey) * sizey)
			{
				prevx = (int)Math.Floor(pt.Position.X / sizex) * sizex;
				prevy = (int)Math.Floor(pt.Position.Y / sizey) * sizey;

				SetChunk(0, 0, 0);
				SetChunk(1, sizex, 0);
				SetChunk(2, -sizex, 0);
				SetChunk(3, 0, sizey);
				SetChunk(4, 0, -sizey);
				SetChunk(5, sizex, sizey);
				SetChunk(6, -sizex, sizey);
				SetChunk(7, sizex, -sizey);
				SetChunk(8, -sizex, -sizey);
			}
		}
		async void SetChunk(int i, int sx, int sy)
		{
			await Task.Run(() =>
			{
				noise[i].GetComponent<Sprite>().Texture = GenerateNoiseTexture(1, (prevx + sx) / sizex, (prevy + sy) / sizey);
				noise[i].GetComponent<Transform>().Position = new Vector2(prevx + sx, prevy + sy);
			});
		}
	}
}
