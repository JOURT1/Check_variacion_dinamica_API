using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine; // <-- Necesario para usar GameObject y Vector3

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player is spawned after dying.
    /// </summary>
    public class PlayerSpawn : Simulation.Event<PlayerSpawn>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var player = model.player;
            player.collider2d.enabled = true;
            player.controlEnabled = false;

            if (player.audioSource && player.respawnAudio)
                player.audioSource.PlayOneShot(player.respawnAudio);

            player.health.Increment();

            // Aquí usamos la posición del checkpoint si existe
            Vector3 spawnPosition = model.spawnPoint.transform.position;

            if (GameController.Instance != null)
            {
                spawnPosition = GameController.Instance.GetCheckpointPosition();
            }

            player.Teleport(spawnPosition);

            player.jumpState = PlayerController.JumpState.Grounded;
            player.animator.SetBool("dead", false);
            model.virtualCamera.m_Follow = player.transform;
            model.virtualCamera.m_LookAt = player.transform;

            Simulation.Schedule<EnablePlayerInput>(2f);
        }
    }
}
