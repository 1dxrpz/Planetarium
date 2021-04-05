﻿using GameEngineTK.Engine.Components;
using GameEngineTK.Engine.Prototypes;
using GameEngineTK.Engine.Prototypes.Enums;
using GameEngineTK.Engine.Prototypes.Interfaces;
using GameEngineTK.Engine.Rendering;
using GameEngineTK.Engine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Planetarium.Engine.Prototypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTK.Engine
{
	public class Params
	{
		public VisibleState isVisible = VisibleState.Visible;
		public bool isDraggable = false;
		public bool MouseDown = false;
		public bool onHover = false;
	}
	public class GameObject : RenderingObjects, IGameInstances
	{
		private readonly List<IComponentManager> Components = new List<IComponentManager>();
		private readonly List<VisualEffect> Effects = new List<VisualEffect>();
		private Layer ParentLayer;
		private string InstanceName;
		private VisibleState visible = VisibleState.Visible;


		public Params objectParams = new Params();
		/// <summary>
		/// Creates new GameObject instance
		/// </summary>
		public GameObject()
		{
			Components.Add(new Transform());
			Components.Add(new Renderer());
			EnsureDefaults();
		}
		public void EnsureDefaults()
		{
			var config = ConfigReader.Parse("project");
			if (config.ContainsKey("EnsureDefaults") && ConfigReader.GetBool(config, "EnsureDefaults"))
			{
				ScriptManager.DefaultLayer.Add(this);
			}
		}

		// REWRITE TO TRANSFORM FIELD
		public void RotateTowardPosition(Vector2 pos)
		{
			this.GetComponent<Transform>().Rotation = (float)Math.Atan2(pos.Y - this.GetComponent<Transform>().ScreenPosition().Y, pos.X - this.GetComponent<Transform>().ScreenPosition().X);
		}
		public void RotateTowardObject(GameObject obj)
		{
			this.RotateTowardPosition(obj.GetComponent<Transform>().Position);
		}
		public void RotateClockwise(float angle)
		{
			this.GetComponent<Transform>().Rotation += angle;
		}
		public void RotateCounterClockwise(float angle)
		{
			this.GetComponent<Transform>().Rotation -= angle;
		}
		// REWRITE TO TRANSFORM FIELD

		public Layer Parent
		{
			get
			{
				return ParentLayer;
			}

			set
			{
				ParentLayer = value;
			}
		}
		public string name
		{
			get
			{
				return InstanceName;
			}

			set
			{
				
				InstanceName = value;
			}
		}

		public bool AddComponent(IComponentManager c)
		{
			if (this.HasComponent(c))
				return false;
			Components.Add(c);
			return true;
		}
		public bool RemoveComponent(IComponentManager c)
		{
			if (!HasComponent(c))
				return false;
			Components.Remove(c);
			return true;
		}

		public T GetComponent<T>()
		{
			for (int i = 0; i < Components.Count; i++)
			{
				if (Components[i] is T)
					return (T)Components[i];
			}
			throw new Exception($"{this} does not contain {nameof(T)} component");
		}
		public bool HasComponent<T>()
		{
			for (int i = 0; i < Components.Count; i++)
				if (Components[i] is T) return true;
			return false;
		}
		public bool HasComponent(IComponentManager obj)
		{
			return Components.Contains(obj);
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

		public SpriteEffects Flip = SpriteEffects.None;

		public bool AddEffect(VisualEffect e)
		{
			if (HasEffect(e))
				return false;
			Effects.Add(e);
			return true;
		}
		public bool RemoveEffect(VisualEffect e)
		{
			if (!HasEffect(e))
				return false;
			Effects.Remove(e);
			return true;
		}
		public bool HasEffect(VisualEffect e)
		{
			return Effects.Contains(e);
		}

		// MAKE FUNCTIONS COMPONENT
		public bool OnObjectClicked()
		{
			bool MouseDown = objectParams.MouseDown;
			if (Mouse.GetState().LeftButton == ButtonState.Pressed && MouseDown)
				return false;
			else
			{
				if (!MouseDown && this.IsHover() && Mouse.GetState().LeftButton == ButtonState.Pressed)
					MouseDown = true;
			}
			return MouseDown;
		} // crap
		public Vector2 OriginPosition = new Vector2();
		
		// REWRITE TO TRANSFORM FIELD
		public bool OnObjectDragging()
		{
			if (this.IsHover() && Mouse.GetState().LeftButton == ButtonState.Released)
				objectParams.onHover = true;
			else if (Mouse.GetState().LeftButton == ButtonState.Released)
				objectParams.onHover = false;
			return objectParams.onHover && Mouse.GetState().LeftButton == ButtonState.Pressed && objectParams.isVisible == VisibleState.Visible;
		}
		public bool IsHover()
		{
			Transform _t = this.GetComponent<Transform>();
			Vector2 pos = _t.Position;
			return Mouse.GetState().X > pos.X &&
				Mouse.GetState().X < pos.X + _t.Width &&
				Mouse.GetState().Y > pos.Y &&
				Mouse.GetState().Y < pos.Y + _t.Width && objectParams.isVisible == VisibleState.Visible;
		}

		// MAKE FUNCTIONS COMPONENT

		// TEST THIS OPTION FOR OPTIMISATION ISSUES
		public float Distance(GameObject obj)
		{
			Vector2 opos = obj.GetComponent<Transform>().Position - obj.OriginPosition;
			Vector2 pos = this.GetComponent<Transform>().Position - OriginPosition;
			return (float) Math.Sqrt((pos.X - opos.X) * (pos.X - opos.X) + (pos.Y - opos.Y) * (pos.Y - opos.Y));
		}
		public float VDistance(GameObject obj)
		{
			Vector2 opos = obj.GetComponent<Transform>().Position;
			Vector2 pos = this.GetComponent<Transform>().Position;
			return Vector2.Distance(opos, pos);
		}
		// TEST THIS OPTION FOR OPTIMISATION ISSUES
		public void init()
		{
			if (Components.Count > 0) Components.ForEach(v => { if (v.Parent != this) v.Parent = this; v.init(); });
		}
		async void r()
		{
			await Task.Run(() => {
				if (Components.Count > 0) Components.ForEach(v =>
				{
					if (!(v is Renderer))
						v.Update();
				});
			});
		}
		public void Draw()
		{
			r();
			GetComponent<Renderer>().Update();
		}
	}
}