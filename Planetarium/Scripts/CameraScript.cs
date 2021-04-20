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
		ProjectSettings settings;
		Vector2 center;

		public void Start()
		{
			settings = ScriptManager.Services.GetService<ProjectSettings>();
			center = new Vector2(settings.WindowWidth, settings.WindowHeight) / 2;
		}
		
		public void Update()
		{
			debug = ScriptManager.Services.GetService<Debug>();
			debug.AddDebugLine($"FPS: {debug.FPS}");
		}
	}
}
