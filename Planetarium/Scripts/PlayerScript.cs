using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GameEngineTK.Engine;
using GameEngineTK.Engine.Components;
using GameEngineTK.Engine.Prototypes;
using GameEngineTK.Engine.Prototypes.Enums;
using GameEngineTK.Engine.Prototypes.Interfaces;
using GameEngineTK.Engine.Rendering;
using GameEngineTK.Engine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Utilities;

namespace GameEngineTK.Scripts
{
	public class PlayerScript : IScriptManager
	{
		public static GameObject Planet;

		TextureHandler planet = new TextureHandler(@"\Planetarium\Content\buffered.png");
		GameObject[] stars;
		public void Start()
		{
			stars = new GameObject[100];
			for (int i = 0; i < 1000; i++)
			{
				stars[i] = new GameObject();
			}

			Layer background = new Layer("bg");
			Color[] data = new Color[1] { new Color(12, 37, 53) };
			Texture2D texture = new Texture2D(ScriptManager.graphicsDevice, 1, 1);
			texture.SetData(data);

			Planet = new GameObject();
			Planet.AddComponent(new BufferedAnimatedSprite());
			BufferedAnimation planetAn = Planet.GetComponent<BufferedAnimatedSprite>().animation;
			planetAn.SpriteSheet = planet.ToTexture2D();

			Planet.GetComponent<Transform>().Width = 280;
			Planet.GetComponent<Transform>().Height = 280;

			planetAn.FrameCount = 100;
			planetAn.FrameSize = new Point(100, 100);
			planetAn.AnimationSpeed = 1;
			planetAn.FrameDepth = 10;

			
		}
		public void Update()
		{
			
		}
	}
}