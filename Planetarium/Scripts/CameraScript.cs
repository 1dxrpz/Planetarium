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
		public void Start()
		{
			
		}
		
		public void Update()
		{
			ProjectSettings settings = ScriptManager.Services.GetService<ProjectSettings>();			
			debug = ScriptManager.Services.GetService<Debug>();
			debug.AddDebugLine($"FPS: {debug.FPS}");
			Vector2 pos = new Vector2(50, 50);

			Camera.Position = PlayerScript.Planet.GetComponent<Transform>().Position + new Vector2(settings.WindowWidth, settings.WindowHeight);

		}
	}
}
