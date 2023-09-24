using Godot;
using System;

public partial class bird : CharacterBody2D
{
	public const float JumpVelocity = -400.0f;
	public const float rotateSpeed = 2.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	public Vector2 velocity;
	
	public bool isStarted = false;
	
	public override void _Ready(){
		velocity = Velocity;
	}
	
	public override void _PhysicsProcess(double delta)
	{		
		// Handle Jump.
		if (Input.IsActionJustPressed("jump")){
			if(!isStarted){
				isStarted = true;
			}
			
			velocity.Y = JumpVelocity;
			Rotation = ConvertDegreesToRadians(-30);
		}
		
		if(!isStarted){
			return;
		}
		
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;		
			
		MoveAndSlide();
		
		rotate_bird();
		
		Velocity = velocity;
	}
	
	public static float ConvertRadiansToDegrees(float radians)
	{
		float degrees = (180 / (float)Math.PI) * radians;
		return (degrees);
	}
	
	public static float ConvertDegreesToRadians(float degrees)
	{
		float radians = (degrees * ((float)Math.PI / 180));
		return radians;
	}	
	
	public void rotate_bird(){		
		if(velocity.Y > 0 && ConvertRadiansToDegrees(Rotation) < 90){
			Rotation += rotateSpeed * ConvertDegreesToRadians(1);
		}else if(velocity.Y < 0 && ConvertRadiansToDegrees(Rotation) > -30){
			Rotation -= rotateSpeed * ConvertDegreesToRadians(1);
		}	
	}
}
