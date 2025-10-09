using Core.Presenters;
using JetBrains.Annotations;
using PlayerInput.Models;
using PlayerInput.Providers;
using PlayerInput.Views;
using UnityEngine;

namespace PlayerInput.Presenters
{
    [UsedImplicitly]
    public class PlayerInputPresenter : BasePresenter<IPlayerInputModel, IPlayerInputProvider, IPlayerInputView>, IPlayerInputPresenter
    {
        public PlayerInputPresenter(IPlayerInputProvider playerInputProvider, IPlayerInputModel playerInputModel)
            : base(playerInputModel, playerInputProvider)
        {
        }

        protected override IPlayerInputView GetViewFromProvider()
        {
            return Provider.GetPlayerInputView();
        }

        protected override void OnViewCreated()
        {
            View.OnJumpClicked += OnJumpHandler;
            View.OnMoveChanged += OnMoveHandler;
        }

        protected override void OnBeforeDispose()
        {
            View.OnJumpClicked -= OnJumpHandler;
            View.OnMoveChanged -= OnMoveHandler;
            Provider.Dispose();
        }

        private void OnJumpHandler(bool jump)
        {
            Model.Jump(jump);
        }

        private void OnMoveHandler(Vector2 move)
        {
            Model.SetMove(move);
        }
    }
}
