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
		public static GameObject Player;
		public void Start()
		{
			Color[] data = new Color[1] { Color.Red };
			Texture2D texture = new Texture2D(ScriptManager.graphicsDevice, 1, 1);
			texture.SetData(data);
			Player = new GameObject();
			Player.AddComponent(new Sprite());
			Player.GetComponent<Sprite>().Texture = texture;
			Player.GetComponent<Transform>().Width = 25;
			Player.GetComponent<Transform>().Height = 25;
		}
		float speed = .5f;
		public void Update()
		{
			if (Keyboard.GetState().IsKeyDown(Keys.D))
				Player.GetComponent<Transform>().TranslateX(speed * Time.deltaTime);
			if (Keyboard.GetState().IsKeyDown(Keys.A))
				Player.GetComponent<Transform>().TranslateX(-speed * Time.deltaTime);
			if (Keyboard.GetState().IsKeyDown(Keys.W))
				Player.GetComponent<Transform>().TranslateY(-speed * Time.deltaTime);
			if (Keyboard.GetState().IsKeyDown(Keys.S))
				Player.GetComponent<Transform>().TranslateY(speed * Time.deltaTime);

		}
	}
}