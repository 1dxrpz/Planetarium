﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using GameEngineTK.Engine;
using GameEngineTK.Scripts;
using PerlinNoise;
using PerlinNoise.Filters;
using PerlinNoise.Transformers;
using System.Threading.Tasks;
using GameEngineTK.Engine.Rendering;
using GameEngineTK.Engine.Prototypes.Interfaces;
using GameEngineTK.Engine.Prototypes.Enums;
using System.Threading;

namespace GameEngineTK
{
	public class Game1 : Game
	{
		private readonly GraphicsDeviceManager _graphics;
		private SpriteBatch ctx;
		private SpriteFont font;
		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			_graphics.SynchronizeWithVerticalRetrace = default;
			base.IsFixedTimeStep = default;
			BoxCollider.RenderColisionMask = default;
			TargetElapsedTime = TimeSpan.FromMilliseconds(1000 / 60);

		}

		protected override void Initialize()
		{
			Window.Position = Point.Zero;
			base.Initialize();
			Services.AddService<ProjectSettings>(new ProjectSettings());
			Services.AddService<Debug>(new Debug());
			ScriptManager.Services = Services;
			ScriptManager.Content = Content;
			ScriptManager.ctx = ctx;
			ScriptManager.graphicsDevice = GraphicsDevice;
			
			//MediaPlayer.Play(song);
			var config = ConfigReader.Parse("project");
			if (config.ContainsKey("EnsureDefaults") && ConfigReader.GetBool(config, "EnsureDefaults"))
			{
				ScriptManager.DefaultScene = new Scene("DefaultScene");
				ScriptManager.DefaultLayout = new Layout("DefaultLayout");
				ScriptManager.DefaultLayer = new Layer("DefaultLayer");
				ScriptManager.DefaultScene.Add(ScriptManager.DefaultLayout);
				ScriptManager.DefaultLayout.Add(ScriptManager.DefaultLayer);
			}
			Program.scripts.ForEach(v => {
				v.Start();
			});
			
			foreach (Scene scene in Theatre.Scenes)
			{
				if (scene.isVisible == VisibleState.Visible)
					foreach (Layout layout in scene.Objects)
					{
						if (layout.isVisible == VisibleState.Visible)
							foreach (Layer layer in layout.Objects)
							{
								if (layer.isVisible == VisibleState.Visible)
									foreach (IGameInstances instance in layer.Objects)
									{
										if (instance.isVisible == VisibleState.Visible)
											instance.init();
									}
							}
					}
			}
			
		}
		
		protected override void LoadContent()
		{
			BoxCollider.ColliderRenderTexture = Content.Load<Texture2D>("SolidWall");

			ctx = new SpriteBatch(GraphicsDevice);
			font = Content.Load<SpriteFont>("font");
			
		}
		public async void r()
		{

			await Task.Run(() => {
				Program.scripts.ForEach(v =>
				{
					v.Update();
				});
			});
		}
		bool test = true;
		protected async override void Update(GameTime gameTime)
		{
			await Task.Run(() => {
				Program.scripts.ForEach(v =>
				{
					v.Update();
				});
				base.Update(gameTime);
			});

			if (test)
			{
				ProjectSettings settings = Services.GetService<ProjectSettings>();
				_graphics.PreferredBackBufferHeight = settings.WindowHeight;
				_graphics.PreferredBackBufferWidth = settings.WindowWidth;
				_graphics.SynchronizeWithVerticalRetrace = settings.VSync;
				BoxCollider.RenderColisionMask = settings.ShowColliders;
				TargetElapsedTime = TimeSpan.FromMilliseconds(1000 / settings.MaxFPS);
				base.IsFixedTimeStep = settings.FixedTS;
				_graphics.ApplyChanges();
				test = false;
			}


			
		}
		protected override void Draw(GameTime gameTime)
		{

			
			ProjectSettings settings = Services.GetService<ProjectSettings>();
			Debug debug = Services.GetService<Debug>();
			GraphicsDevice.Clear(new Color(12, 37, 53));

			debug.Update(gameTime);
			Time.deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
			ctx.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp);
			if (debug.Enabled)
			{
				var config = ConfigReader.Parse("project");
				ctx.DrawString(font, "Project name: " + (config.ContainsKey("name") ? config["name"] : "Unnamed Project"), new Vector2(10, 10), Color.Gray);
				ctx.DrawString(font, "Author: " + (config.ContainsKey("author") ? config["author"] : "Unknown author"), new Vector2(10, 25), Color.Gray);
				ctx.DrawString(font, "Version: " + (config.ContainsKey("version") ? "v.1.00" : config["version"]), new Vector2(10, 40), Color.Gray);
				ctx.DrawString(font, " - Debug.Text\n[scope]: message " + debug.text, new Vector2(10, 60), Color.White);
			}
			
			foreach (Scene scene in Theatre.Scenes)
			{
				if (scene.isVisible == VisibleState.Visible)
					foreach (Layout layout in scene.Objects)
					{
						if (layout.isVisible == VisibleState.Visible)
							foreach (Layer layer in layout.Objects)
							{
								if (layer.isVisible == VisibleState.Visible)
									foreach (IGameInstances instance in layer.Objects)
									{
										if (instance.isVisible == VisibleState.Visible)
										{
											instance.Draw();
										}
									}
							}
					}
			}
			ctx.End();

			base.Draw(gameTime);
		}
	}
}