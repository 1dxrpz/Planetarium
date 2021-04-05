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
		public GameObject bg;

		TextureHandler planet = new TextureHandler(@"\Planetarium\Content\buffered.png");
		GameObject[] gameObjects;
		public void Start()
		{
			gameObjects = new GameObject[1000];
			for (int i = 0; i < 100; i++)
			{
				gameObjects[i] = new GameObject();
				
			}

			Layer background = new Layer("bg");
			Color[] data = new Color[1] { new Color(12, 37, 53) };
			Texture2D texture = new Texture2D(ScriptManager.graphicsDevice, 1, 1);
			texture.SetData(data);
			bg = new GameObject();

			background.Add(bg);

			bg.AddComponent(new Sprite());
			bg.GetComponent<Sprite>().Texture = texture;
			bg.GetComponent<Transform>().Width = 1000;
			bg.GetComponent<Transform>().Height = 1000;

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