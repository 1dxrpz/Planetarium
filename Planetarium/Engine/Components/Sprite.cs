using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GameEngineTK.Engine.Prototypes.Interfaces;
using GameEngineTK.Engine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngineTK.Engine.Components
{
	class BufferedSprite
	{

	}
	class Sprite : IComponentManager
	{
		private int width, height;
		private Vector2 position;
		private GameObject parent;
		private Texture2D texture;

		public GameObject Parent
		{
			get
			{
				return parent;
			}

			set
			{
				parent = value;
			}
		}
		public Vector2 Position
		{
			get
			{
				return position;
			}

			set
			{
				position = position;
			}
		}
		public int Width
		{
			get
			{
				return width;
			}

			set
			{
				width = value;
			}
		}
		public int Height
		{
			get
			{
				return height;
			}

			set
			{
				height = value; 
			}
		}

		public Texture2D Texture;
		public void Update()
		{
			width = parent.GetComponent<Transform>().Width;
			height = parent.GetComponent<Transform>().Height;
		}

		public void init()
		{
			
		}
	}
}
