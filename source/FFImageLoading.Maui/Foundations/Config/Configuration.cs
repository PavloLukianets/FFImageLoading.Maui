﻿using System;
using System.Net.Http;
using FFImageLoading.Helpers;
using FFImageLoading.Cache;
using FFImageLoading.Work;
using System.Net;

namespace FFImageLoading.Config
{
	/// <summary>
	/// Configuration.
	/// </summary>
	public class Configuration : IConfiguration
	{
		public Configuration()
		{
			// default values here:
			BitmapOptimizations = true;
			FadeAnimationEnabled = true;
			FadeAnimationForCachedImages = false; // by default cached images will not fade when displayed on screen, otherwise it gives the impression that UI is laggy
			FadeAnimationDuration = 300;
			TransformPlaceholders = true;
			DownsampleInterpolationMode = InterpolationMode.Default;
			HttpHeadersTimeout = 3;
			HttpReadTimeout = 15;
			HttpReadBufferSize = 8192;
			HttpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
			HttpClient.Timeout = TimeSpan.FromSeconds(HttpReadTimeout);
			VerbosePerformanceLogging = false;
			VerboseMemoryCacheLogging = false;
			VerboseLoadingCancelledLogging = false;
			VerboseLogging = false;
			DecodingMaxParallelTasks = Environment.ProcessorCount <= 2 ? 1 : 2;
			SchedulerMaxParallelTasks = Math.Max(16, 4 + Environment.ProcessorCount);
			DiskCacheDuration = TimeSpan.FromDays(30d);
			TryToReadDiskCacheDurationFromHttpHeaders = true;
			ExecuteCallbacksOnUIThread = false;
			StreamChecksumsAsKeys = true;
			AnimateGifs = true;
			DelayInMs = 10; //Task.Delay resolution is around 15ms
			ClearMemoryCacheOnOutOfMemory = true;
			InvalidateLayout = true;
		}

		/// <summary>
		/// Gets or sets the http client used for web requests.
		/// </summary>
		/// <value>The http client used for web requests.</value>
		public HttpClient HttpClient { get; set; }

		/// <summary>
		/// Gets or sets the disk cache path.
		/// </summary>
		/// <value>The disk cache path.</value>
		public string DiskCachePath { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:FFImageLoading.Config.Configuration"/> bitmap
		/// memory optimizations.
		/// </summary>
		/// <value><c>true</c> if bitmap memory optimizations; otherwise, <c>false</c>.</value>
		public bool BitmapOptimizations { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:FFImageLoading.Config.Configuration"/> stream
		/// checksums as keys.
		/// </summary>
		/// <value><c>true</c> if stream checksums as keys; otherwise, <c>false</c>.</value>
		public bool StreamChecksumsAsKeys { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="FFImageLoading.Config.Configuration"/> fade animation enabled.
		/// </summary>
		/// <value><c>true</c> if fade animation enabled; otherwise, <c>false</c>.</value>
		public bool FadeAnimationEnabled { get; set; }

		/// <summary>
		/// Gets or sets a value indicating wheter fade animation for
		/// cached or local images should be enabled.
		/// </summary>
		/// <value><c>true</c> if fade animation for cached or local images; otherwise, <c>false</c>.</value>
		public bool FadeAnimationForCachedImages { get; set; }

		/// <summary>
		/// Gets or sets the default duration of the fade animation in ms.
		/// </summary>
		/// <value>The duration of the fade animation.</value>
		public int FadeAnimationDuration { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="FFImageLoading.Config.Configuration"/> transforming place is enabled.
		/// </summary>
		/// <value><c>true</c> if transform should be applied to placeholder images; otherwise, <c>false</c>.</value>
		public bool TransformPlaceholders { get; set; }

		/// <summary>
		/// Gets or sets default downsample interpolation mode.
		/// </summary>
		/// <value>downsample interpolation mode</value>
		public InterpolationMode DownsampleInterpolationMode { get; set; }

		/// <summary>
		/// Gets or sets a value that determines whether upscaling should be used in <see cref="FFImageLoading.Work.TaskParameter.DownSample"/>/<see cref="FFImageLoading.Work.TaskParameter.DownSampleInDip"/> functions if the image is smaller than passed dimensions
		/// </summary>
		/// <value><c>true</c> if upscaling is allowed; otherwise, <c>false</c></value>
		public bool AllowUpscale { get; set; }

		/// <summary>
		/// Gets or sets the maximum time in seconds to wait to receive HTTP headers before the HTTP request is cancelled.
		/// </summary>
		/// <value>The http connect timeout.</value>
		public int HttpHeadersTimeout { get; set; }

		/// <summary>
		/// Gets or sets the maximum time in seconds to wait before the HTTP request is cancelled.
		/// </summary>
		/// <value>The http read timeout.</value>
		public int HttpReadTimeout { get; set; }

		/// <summary>
		/// Gets or sets the size of the http read buffer.
		/// </summary>
		/// <value>The size of the http read buffer.</value>
		public int HttpReadBufferSize { get; set; }

		/// <summary>
		/// Gets or sets the maximum size of the memory cache in bytes.
		/// </summary>
		/// <value>The maximum size of the memory cache in bytes.</value>
		public int MaxMemoryCacheSize { get; set; }

		/// <summary>
		/// Milliseconds to wait prior to start any task.
		/// </summary>
		public int DelayInMs { get; set; }

		/// <summary>
		/// Enables / disables verbose performance logging.
		/// WARNING! It will downgrade image loading performance, disable it for release builds!
		/// </summary>
		/// <value>The verbose performance logging.</value>
		public bool VerbosePerformanceLogging { get; set; }

		/// <summary>
		/// Enables / disables verbose memory cache logging.
		/// WARNING! It will downgrade image loading performance, disable it for release builds!
		/// </summary>
		/// <value>The verbose memory cache logging.</value>
		public bool VerboseMemoryCacheLogging { get; set; }

		/// <summary>
		/// Enables / disables verbose image loading cancelled logging.
		/// WARNING! It will downgrade image loading performance, disable it for release builds!
		/// </summary>
		/// <value>The verbose image loading cancelled logging.</value>
		public bool VerboseLoadingCancelledLogging { get; set; }

		/// <summary>
		/// Enables / disables  verbose logging.
		/// IMPORTANT! If it's disabled are other verbose logging options are disabled too
		/// </summary>
		/// <value>The verbose logging.</value>
		public bool VerboseLogging { get; set; }

		/// <summary>
		/// Gets or sets the scheduler max parallel tasks.
		/// Default: Environment.ProcessorCount <= 2 ? 1 : 2;
		/// </summary>
		/// <value>The decoding max parallel tasks.</value>
		public int DecodingMaxParallelTasks { get; set; }

		/// <summary>
		/// Gets or sets the scheduler max parallel tasks.
		/// Default: Math.Max(16, 4 + Environment.ProcessorCount);
		/// </summary>
		/// <value>The scheduler max parallel tasks.</value>
		public int SchedulerMaxParallelTasks { get; set; }

		/// <summary>
		/// Gets or sets the scheduler max parallel tasks factory.
		/// If null SchedulerMaxParallelTasks property is used
		/// </summary>
		/// <value>The scheduler max parallel tasks factory.</value>
		public Func<IConfiguration, int> SchedulerMaxParallelTasksFactory { get; set; }

		/// <summary>
		/// Gets or sets the default duration of the disk cache entries.
		/// </summary>
		/// <value>The duration of the cache.</value>
		public TimeSpan DiskCacheDuration { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether try to read
		/// disk cache duration from http headers .
		/// </summary>
		/// <value><c>true</c> if try to read disk cache duration from http headers; otherwise, <c>false</c>.</value>
		public bool TryToReadDiskCacheDurationFromHttpHeaders { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether callbacs (OnFinish, OnSuccess, etc)
		/// should execute on UI thread
		/// </summary>
		/// <value><c>true</c> if execute callbacks on UIT hread; otherwise, <c>false</c>.</value>
		public bool ExecuteCallbacksOnUIThread { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether image loader should animate gifs.
		/// </summary>
		/// <value><c>true</c> if animate gifs; otherwise, <c>false</c>.</value>
		public bool AnimateGifs { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether clear
		/// memory cache on out of memory.
		/// </summary>
		/// <value><c>true</c> if clear memory cache on out of memory; otherwise, <c>false</c>.</value>
		public bool ClearMemoryCacheOnOutOfMemory { get; set; }

		/// <summary>
		/// Specifies if view layout should be invalidated after image is loaded
		/// </summary>
		/// <value><c>true</c> if invalidate layout; otherwise, <c>false</c>.</value>
		public bool InvalidateLayout { get; set; }

		/// <summary>
		/// Update IC Configuration instance's properties
		/// </summary>
		/// <param name="newConfig">custom config</param>
		/// <returns></returns>
		public bool UpdateConfig(IConfiguration newConfig)
		{
			if (this == newConfig)
			{
				return true;
			}

			BitmapOptimizations = newConfig.BitmapOptimizations;
			FadeAnimationEnabled = newConfig.FadeAnimationEnabled;
			FadeAnimationForCachedImages = newConfig.FadeAnimationForCachedImages;
			FadeAnimationDuration = newConfig.FadeAnimationDuration;
			TransformPlaceholders = newConfig.TransformPlaceholders;
			DownsampleInterpolationMode = newConfig.DownsampleInterpolationMode;
			HttpHeadersTimeout = newConfig.HttpHeadersTimeout;
			HttpReadTimeout = newConfig.HttpReadTimeout;
			HttpReadBufferSize = newConfig.HttpReadBufferSize;
			HttpClient = newConfig.HttpClient;
			HttpClient.Timeout = newConfig.HttpClient.Timeout;
			VerbosePerformanceLogging = newConfig.VerbosePerformanceLogging;
			VerboseMemoryCacheLogging = newConfig.VerboseMemoryCacheLogging;
			VerboseLoadingCancelledLogging = newConfig.VerboseLoadingCancelledLogging;
			VerboseLogging = newConfig.VerboseLogging;
			DecodingMaxParallelTasks = newConfig.DecodingMaxParallelTasks;
			SchedulerMaxParallelTasks = newConfig.SchedulerMaxParallelTasks;
			DiskCacheDuration = newConfig.DiskCacheDuration;
			TryToReadDiskCacheDurationFromHttpHeaders = newConfig.TryToReadDiskCacheDurationFromHttpHeaders;
			ExecuteCallbacksOnUIThread = newConfig.ExecuteCallbacksOnUIThread;
			StreamChecksumsAsKeys = newConfig.StreamChecksumsAsKeys;
			AnimateGifs = newConfig.AnimateGifs;
			DelayInMs = newConfig.DelayInMs;
			ClearMemoryCacheOnOutOfMemory = newConfig.ClearMemoryCacheOnOutOfMemory;
			InvalidateLayout = newConfig.InvalidateLayout;

			return true;
		}
	}
}

