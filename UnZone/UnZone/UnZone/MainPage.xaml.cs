using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnZone.Controls;
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

        private async void TimeCellTapRecognizer_Tapped(object sender, EventArgs e)
        {
            // fade in the overlay
            FadeBackground.Opacity = 0;
            FadeBackground.IsVisible = true;
            _ = FadeBackground.FadeTo(1, animationSpeed);

            // get the cell that was tapped
            var element = (TimeCell)sender;

            // position the overlay
            FakeCell.BindingContext = element.BindingContext;

            var dropDownContainerRect = new Rectangle(
                0,
                element.Bounds.Top,
                this.Width,
                FakeCell.Height + DeleteDropDown.Height + InfoDropDown.Height);
            AbsoluteLayout.SetLayoutBounds(DropDownContainer, dropDownContainerRect);

            // hide the dropdowns

            FrontSide.IsVisible = true;
            DeleteDropDown.IsVisible = false;
            EditDropDown.IsVisible = false;
            InfoDropDown.IsVisible = false;
            BackSide.IsVisible = false;


            await OpenDropDown(DeleteDropDown);
            await OpenDropDown(EditDropDown);
            await OpenDropDown(InfoDropDown);


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


        private async void BackgroundTapRecognizer_Tapped(object sender, EventArgs e)
        {
            // close the drop downs
            await CloseDropDown(InfoDropDown);
            await CloseDropDown(EditDropDown);
            await CloseDropDown(DeleteDropDown);

            // fade out the overlay
            await FadeBackground.FadeTo(0, animationSpeed);
            FadeBackground.IsVisible = false;


        }

        private async void DeleteTapRecognizer_Tapped(object sender, EventArgs e)
        {
            await CloseDropDown(InfoDropDown);

            FakeCell.IsVisible = true;
           
            await DropDownContainer.RotateYTo(-90, animationSpeed, Easing.SpringIn);
            DropDownContainer.RotationY = 90;
            BackSide.IsVisible = true;
            FrontSide.IsVisible = false;
            await DropDownContainer.RotateYTo(0, animationSpeed, Easing.SpringOut);
        }



        private async void NoButtonGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            // flip back to the front
            await DropDownContainer.RotateYTo(-90, animationSpeed, Easing.SpringIn);
            DropDownContainer.RotationY = 90;
            FrontSide.IsVisible = true;
            BackSide.IsVisible = false;
            await DropDownContainer.RotateYTo(0, animationSpeed, Easing.SpringOut);

            await OpenDropDown(InfoDropDown);

        }
    }
}
