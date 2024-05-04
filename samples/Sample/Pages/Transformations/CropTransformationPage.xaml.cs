﻿using System;
using System.Collections.Generic;
using FFImageLoading;
using FFImageLoading.Helpers;
using FFImageLoading.Transformations;

namespace Sample
{
	public partial class CropTransformationPage : ContentPage
	{
		private readonly IImageService _imageService = ServiceHelper.GetService<IImageService>();

		CropTransformationPageModel viewModel;

		public CropTransformationPage()
		{
			InitializeComponent();
			BindingContext = viewModel = new CropTransformationPageModel
			{
				ReloadImageHandler = () =>
				{
					image.ReloadImage();
					image.LoadingPlaceholder = null;
				}
			};
		}

		void OnPanUpdated(object sender, PanUpdatedEventArgs args)
		{
			(this.BindingContext as CropTransformationPageModel)?.OnPanUpdated(args);
		}

		void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs args)
		{
			(this.BindingContext as CropTransformationPageModel)?.OnPinchUpdated(args);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			viewModel.ReloadImage();
		}

		private void OnTestPngClicked(object sender, EventArgs e)
		{
			viewModel.ImageUrl = "no_avatar.png";
			viewModel.ReloadImage();
		}

		private async void TestCropSize(object sender, EventArgs e)
		{
			if (_imageService == null) return;

			var imagePath = "maui_beach.jpg";
			var original = await _imageService.LoadFile(imagePath).AsJPGStreamAsync(_imageService, 100);
			var jpgStream = await _imageService
				.LoadFile(imagePath)
				.Transform(new CropTransformation(1, 0, 0))
				.AsJPGStreamAsync(_imageService, 100);
			viewModel.OriginalSize = original.Length;
			viewModel.CroppedSize = jpgStream.Length;
		}
	}
}
