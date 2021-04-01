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
		public static GameObject Nebula;
		
		TextureHandler planet = new TextureHandler(@"\Planetarium\Content\buffered.png");
		TextureHandler nebula = new TextureHandler(@"\Planetarium\Content\nebula.png");

		public void Start()
		{
			Planet = new GameObject();
			Planet.AddComponent(new BufferedAnimatedSprite());
			BufferedAnimation planetAn = Planet.GetComponent<BufferedAnimatedSprite>().animation;
			planetAn.SpriteSheet = planet.ToTexture2D();

			Nebula = new GameObject();
			Nebula.AddComponent(new Sprite());
			Sprite nebulaSp = Nebula.GetComponent<Sprite>();
			nebulaSp.Texture = nebula.ToTexture2D();
			Transform nebulaTr = Nebula.GetComponent<Transform>();
			nebulaTr.Width = 130 * 10;
			nebulaTr.Height = 126 * 10;
			nebulaTr.Position = new Vector2(200, 200);

			Planet.GetComponent<Transform>().Width = 280;
			Planet.GetComponent<Transform>().Height = 280;


			planetAn.FrameCount = 100;
			planetAn.FrameSize = new Point(100, 100);
			planetAn.AnimationSpeed = 1;
			planetAn.FrameDepth = 10;
			Planet.isVisible = VisibleState.Visible;
			Nebula.isVisible = VisibleState.Visible;

			Nebula.MoveTo(0);
		}
		public void Update()
		{
			ScriptManager.Services.GetService<Debug>().AddDebugLine("dt: " + Time.deltaTime);

			/*
			
			*/
		}
	}
}