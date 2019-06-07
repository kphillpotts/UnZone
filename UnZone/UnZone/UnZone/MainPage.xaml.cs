using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnZone.Controls;
using UnZone.Models;
using UnZone.ViewModels;
using Xamarin.Forms;

namespace UnZone
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainViewModel();
        }

        uint animationSpeed = 300;
        TimeCell selectedElement;
        int selectedIndex;
        private async void TimeCellTapRecognizer_Tapped(object sender, EventArgs e)
        {
            // get the cell that was tapped
            selectedElement = (TimeCell)sender;
            selectedIndex = ViewStack.Children.IndexOf(selectedElement);

            // set the binding context to the panel
            // so that it can be inherited by the FakeCell and
            // any other controls required on the popup.
            FrontSide.BindingContext = selectedElement.BindingContext;

            // position the drop downs
            PositionDropDown(selectedElement, FrontSide);
            PositionDropDown(selectedElement, BackSide);

            // fade in the overlay
            FadeBackground.Opacity = 0;
            FadeBackground.IsVisible = true;
            _ = FadeBackground.FadeTo(1, animationSpeed);

            // position the close button
            var padding = 40;
            var xPos = (this.Width / 2) - (CloseButton.Width / 2);
            var yPos = selectedElement.Bounds.Y + FrontSide.Bounds.Height + padding;
            var closeButtonRect = new Rectangle(xPos, yPos, CloseButton.Width, CloseButton.Height);
            AbsoluteLayout.SetLayoutBounds(CloseButton, closeButtonRect);

            // hide the dropdowns
            FrontSide.IsVisible = true;
            DeleteDropDown.IsVisible = false;
            EditDropDown.IsVisible = false;
            InfoDropDown.IsVisible = false;
            BackSide.IsVisible = false;
            CloseButton.Opacity = 0;

            await OpenDropDown(DeleteDropDown);
            await OpenDropDown(EditDropDown);
            await OpenDropDown(InfoDropDown);

            AnimateCloseButton(CloseButton, entering:true);

        }

        private void AnimateCloseButton(VisualElement elementToTransform, bool entering)
        {
            var startingTranslation = entering ? 100 : 0;
            var endingTranslation = entering ? 0 : 100;
            var translationEasing = entering ? Easing.SpringOut : Easing.SinIn;

            var startingOpacity = entering ? 0 : 1;
            var endingOpacity = entering ? 1 : 0;

            var startingRotation = entering ? -90 : 0;
            var endingRotation = entering ? 0 : 180;

            elementToTransform.TranslationY = startingTranslation;
            elementToTransform.Opacity = startingOpacity;
            elementToTransform.Rotation = startingRotation;

            elementToTransform.FadeTo(endingOpacity, 500);
            elementToTransform.RotateTo(endingRotation, 700, Easing.SinInOut);
            elementToTransform.TranslateTo(0, endingTranslation, 600, translationEasing);
        }

        private void PositionDropDown(VisualElement parent, VisualElement dropDown)
        {
            AbsoluteLayout.SetLayoutFlags(dropDown, AbsoluteLayoutFlags.None);
            var dropDownContainerRect = new Rectangle(0, parent.Bounds.Top, this.Width, dropDown.Height);
            AbsoluteLayout.SetLayoutBounds(dropDown, dropDownContainerRect);
        }

        private async Task OpenDropDown(View view)
        {
            view.IsVisible = true;
            view.RotationX = -90;
            view.Opacity = 0;
            _ = view.FadeTo(1, animationSpeed);
            await view.RotateXTo(0, animationSpeed);
        }

        private async Task CloseDropDown(View view)
        {
            _ = view.FadeTo(0, animationSpeed);
            await view.RotateXTo(-90, animationSpeed);
            view.IsVisible = false;
        }

        private async void DeleteTapRecognizer_Tapped(object sender, EventArgs e)
        {
            await CloseDropDown(InfoDropDown);
            AnimateCloseButton(CloseButton, entering: false);

            FakeCell.IsVisible = true;
            await Flip(FrontSide, BackSide);
        }

        private async Task Flip (VisualElement from, VisualElement to)
        {
            await from.RotateYTo(-90, animationSpeed, Easing.SpringIn);
            to.RotationY = 90;
            to.IsVisible = true;
            from.IsVisible = false;
            await to.RotateYTo(0, animationSpeed, Easing.SpringOut);
        }

        private async void NoButtonGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            // flip back to the front
            await Flip(BackSide, FrontSide);
            AnimateCloseButton(CloseButton, entering: true);
            await OpenDropDown(InfoDropDown);

        }

        private async void CloseGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            AnimateCloseButton(CloseButton, entering: false);

            // close the drop downs
            await CloseDropDown(InfoDropDown);
            await CloseDropDown(EditDropDown);
            await CloseDropDown(DeleteDropDown);

            // fade out the overlay
            await FadeBackground.FadeTo(0, animationSpeed);
            FadeBackground.IsVisible = false;

        }

        private async void YesButtonTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            // close the overlays
            await Flip(BackSide, FrontSide);
            CloseDropDown(EditDropDown);
            await CloseDropDown(DeleteDropDown);

            await FadeBackground.FadeTo(0, animationSpeed);
            FadeBackground.IsVisible = false;

            // go through all the elements after it and translate them up
            List<Task> animations = new List<Task>();

            for (int i = selectedIndex + 1; i < ViewStack.Children.Count; i++)
            {
                VisualElement elementToMove;
                elementToMove = ViewStack.Children[i];
                // work out the bounds we are going to move them to
                var boundsToMoveTo = elementToMove.Bounds;
                boundsToMoveTo.Top -= selectedElement.Height;
                animations.Add(elementToMove.LayoutTo(boundsToMoveTo, animationSpeed, Easing.BounceOut));
            }
            await Task.WhenAll(animations);
            await selectedElement.FadeTo(0, animationSpeed);
            ViewStack.Children.Remove(selectedElement);
        }
    }
}
