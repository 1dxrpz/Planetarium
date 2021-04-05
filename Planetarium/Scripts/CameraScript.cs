using System;
using System.Collections.Generic;
using System.Text;
using GameEngineTK.Engine;
using GameEngineTK.Engine.Prototypes.Interfaces;
using GameEngineTK.Engine.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngineTK.Scripts
{
	
	public class CameraScript : IScriptManager
	{
		
		Debug debug;
		Transform planet;
		ProjectSettings settings;
		Vector2 center;

		public void Start()
		{
			settings = ScriptManager.Services.GetService<ProjectSettings>();
			planet = PlayerScript.Planet.GetComponent<Transform>();
			center = new Vector2(settings.WindowWidth, settings.WindowHeight) / 2;
			Camera.Position = planet.Position;
		}
		
		public void Update()
		{
			debug = ScriptManager.Services.GetService<Debug>();
			debug.AddDebugLine($"FPS: {debug.FPS}");

			Vector2 pos = planet.Position - center
				+ new Vector2(planet.Width, planet.Height) / 2
				+ (CursorScript.Cursor.GetComponent<Transform>().Position - center) / 10;
			Camera.Position = Vector2.Lerp(Camera.Position,
				pos, .01f);
		}
	}
}
