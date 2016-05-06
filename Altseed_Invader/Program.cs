﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Game
{
	class Program
	{
		[System.STAThread]
		static void Main(string[] args)
		{
			asd.Engine.Initialize("Invader", 480, 640, new asd.EngineOption());

			ControlableObject player = new ControlableObject();
			asd.Engine.AddObject2D(player);

			while (asd.Engine.DoEvents())
			{
				asd.Engine.Update();
			}
			asd.Engine.Terminate();
		}
	}

	class ControlableObject : asd.TextureObject2D
	{
		public ControlableObject()
		{
			Texture = asd.Engine.Graphics.CreateTexture2D("Resources/player.png");
			Position = new asd.Vector2DF(222, 600);
		}

		protected override void OnUpdate()
		{
			asd.Vector2DF pos=Position;
			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Left) == asd.KeyState.Hold)
			{
				pos.X -= 2.0f;
			}

			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Right) == asd.KeyState.Hold)
			{
				pos.X += 2.0f;
			}
			pos.X = asd.MathHelper.Clamp(pos.X,asd.Engine.WindowSize.X - Texture.Size.X,0);
			Position = pos;

			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Space) == asd.KeyState.Push)
			{
				Bullet bullet = new Bullet(Position+new asd.Vector2DF(18.0f,0.0f));
				asd.Engine.AddObject2D(bullet);
			}
		}
	}

	class Bullet :asd.TextureObject2D
	{
		public Bullet(asd.Vector2DF firstPosition)
		{
			Texture = asd.Engine.Graphics.CreateTexture2D("Resources/bullet.png");
			Position = firstPosition;
		}

		protected override void OnUpdate()
		{
			Position = Position - new asd.Vector2DF(0.0f, 5.0f);
		}
	}

}
