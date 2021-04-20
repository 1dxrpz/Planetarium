using System;
using GameEngineTK.Engine;
using GameEngineTK.Engine.Components;
using GameEngineTK.Engine.Prototypes.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PerlinNoise;
using PerlinNoise.Filters;
using PerlinNoise.Transformers;
using Planetarium.Engine.Utils.Noise.PerlinNoise.Filters;

namespace Planetarium.Scripts
{
	class WorldScript : IScriptManager
	{
		PerlinNoiseGenerator NoiseGenerator;
		NoiseField<float> perlinNoise;
		Texture2D noiseTexture;
		public void GenerateNoiseTexture()
		{
			PerlinNoiseGenerator gen = new PerlinNoiseGenerator();
			gen.OctaveCount = 7;
			gen.Persistence = .5f;
			gen.Interpolation = InterpolationAlgorithms.CosineInterpolation;

			perlinNoise = gen.GeneratePerlinNoise(1024, 1024);

			PixellatedColorFilter filter = new PixellatedColorFilter(10);
			Texture2DTransformer transformer = new Texture2DTransformer(ScriptManager.graphicsDevice);

			filter.AddColorPoint(0f, .2f, new Color(100, 100, 100));
			filter.AddColorPoint(.2f, .4f, new Color(120, 120, 120));
			filter.AddColorPoint(.4f, .6f, new Color(140, 140, 140));
			filter.AddColorPoint(.6f, .8f, new Color(160, 160, 160));
			filter.AddColorPoint(.8f, 1f, new Color(180, 180, 180));

			noiseTexture = transformer.Transform(filter.Filter(perlinNoise));
		}
		GameObject noise;
		public void Start()
		{
			GenerateNoiseTexture();

			noise = new GameObject();
			noise.GetComponent<Transform>().Width = 1024;
			noise.GetComponent<Transform>().Height = 1024;
			noise.AddComponent(new Sprite());
			noise.GetComponent<Sprite>().Texture = noiseTexture;

		}
		/// <summary>
		/// Method that updates script every tick
		/// </summary>
		public void Update()
		{
			// Here's your Update code
		}
	}
}
