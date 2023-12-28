using UnityEngine;

public class PlayerInputController : PlayerBase
{
    void Update()
    {
        GatherHorizontalInput();
        GatherJumpInput();
        GatherAttackInput();
    }

    private void GatherHorizontalInput()
    {
        Player.move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (!Player.data.snapInput) return;
        Player.move.x = Mathf.Abs(Player.move.x) < Player.data.horizontalDeadZoneThreshold
            ? 0
            : Mathf.Sign(Player.move.x);
        Player.move.y = Mathf.Abs(Player.move.y) < Player.data.verticalDeadZoneThreshold
            ? 0
            : Mathf.Sign(Player.move.y);
    }

    private void GatherJumpInput()
    {
        Player.jumpPressed = Input.GetButtonDown("Jump");
        Player.jumpHeld = Input.GetButtonDown("Jump");
    }

    private void GatherAttackInput()
    {
        Player.attackPressed = Input.GetButtonDown("Fire1");
    }
}