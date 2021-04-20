using GameEngineTK.Engine.Prototypes.Enums;
using GameEngineTK.Engine.Prototypes.Interfaces;
using GameEngineTK.Engine.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngineTK.Engine
{
	public class GUI : IGameInstances
	{
		
		private VisibleState visible;
		private Layer parentLayer;
		private string Name;
		public bool isHover
		{
			get
			{
				Point mousePos = Mouse.GetState().Position;
				return mousePos.X >= Bounds.X &&
					mousePos.X <= Bounds.X + Bounds.Width &&
					mousePos.Y >= Bounds.Y &&
					mousePos.Y <= Bounds.Y + Bounds.Height;
			}
		}
		public VisibleState isVisible
		{
			get
			{
				return visible;
			}

			set
			{
				visible = value;
			}
		}
		public Layer Parent
		{
			get
			{
				return parentLayer;
			}

			set
			{
				parentLayer = value;
			}
		}
		public void MoveTo(int index)
		{
			Parent.Objects[Parent.Objects.FindIndex(v => v == this)] = Parent.Objects[index];
			Parent.Objects[index] = this;
		}
		public void MoveToLayer(Layer layer)
		{
			Parent.Remove(this);
			layer.Add(this);
		}
		public string name
		{
			get
			{
				return Name;
			}
			set
			{
				if (Parent.Objects.FindIndex(v => v.name == value) != -1)
					throw new Exception($"Object with name {value} already exists.");
				Name = value;
			}
		}

		public Rectangle Bounds;

		public void Draw()
		{
			
		}

		public virtual void Update() { }

		public void init()
		{
			
		}
		public void EnsureDefaults()
		{
			var config = ConfigReader.Parse("project");
			if (config.ContainsKey("EnsureDefaults") && ConfigReader.GetBool(config, "EnsureDefaults"))
			{
				ScriptManager.DefaultLayer.Add(this);
			}
		}
		public GUI()
		{

			EnsureDefaults();
		}
	}
	public class Button : GUI
	{
		public override void Update()
		{
			base.Update();
		}
	}
	public class Slider : GUI
	{
		public override void Update()
		{
			base.Update();
		}
	}
	public class Memo : GUI
	{
		public override void Update()
		{
			base.Update();
		}
	}
}
