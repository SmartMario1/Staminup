using Editor;
using Sandbox;
using System.Linq;
using System.Runtime.CompilerServices;
using TerrorTown;

[Title( "Staminup" ), Category( "Special" )]
[TraitorBuyable( "Special", 1 )]
public class Staminup : Carriable
{
	public override string ViewModelPath => "models/v_t7_perk_bottle.vmdl";
	public override string WorldModelPath => "models/sbox_props/beer_bottle/beer_bottle.vmdl";

	private readonly float DrinkTime = 5.1f;

	private bool started { get; set; } = false;
	private RealTimeSince exist { get; set; }

	[GameEvent.Tick]
	public void GameTick()
	{
		if ( Owner != null)
		{
			var ply = Owner as TerrorTown.Player;
			if ( ply == null ) { return; }
			var mover = ply.MovementController as TerrorTown.WalkController;
			if ( mover == null ) { return; }
			if (!started)
			{
				ply.PlaySound( "staminup" );
				exist = 0;
				mover.SpeedMultiplier += 0.5f;
			}
			started = true;
			if (exist > DrinkTime)
			{
				if (Game.IsServer) Delete();
			}
			
			ply.Inventory.SetActiveSlot(ply.Inventory.Items.IndexOf(this));
		}
	}
}
