using System;
using System.Drawing;
using System.IO;
using NAudio.Wave;

namespace EPlayer.Wave
{
	public class WaveFormRenderer
	{
		#region Settings
		private const int BlockSize = 500;
		private const int Height = 40;
		private const int Width = 600;
		private const int SpacerPixels = 1;
		private const int PixelsPerPeak = 2;
		private static Color BackgroundColor => Color.Transparent;
		public IPeakProvider PeakProvider { get; set; } = new SamplingPeakProvider(BlockSize);
		#endregion

		#region Color Blocks
		public readonly SoundCloudBlockWaveFormSettings OrangeBlocks = new SoundCloudBlockWaveFormSettings(
			topPeakColor: Color.FromArgb(255, 77, 0),
			topSpacerStartColor: Color.FromArgb(255, 77, 0),
			bottomPeakColor: Color.FromArgb(250, 157, 47, 0),
			bottomSpacerColor: Color.FromArgb(0, 79, 79, 79))
			{
				TopHeight = Height,
				BottomHeight = Height / 3,
				Width = Width,
				SpacerPixels = SpacerPixels,
				PixelsPerPeak = PixelsPerPeak,
				BackgroundColor = BackgroundColor
			};

		public readonly SoundCloudBlockWaveFormSettings GrayBlocks = new SoundCloudBlockWaveFormSettings(
			  topPeakColor: Color.FromArgb(254,254,254),
			  topSpacerStartColor: Color.FromArgb(0, 224, 224, 224),
			  bottomPeakColor: Color.FromArgb(150,150,150),
			  bottomSpacerColor: Color.FromArgb(0, 128, 128, 128))
			{
				TopHeight = Height,
				BottomHeight = Height / 3,
				Width = Width,
				SpacerPixels = SpacerPixels,
				PixelsPerPeak = PixelsPerPeak,
				BackgroundColor = BackgroundColor
			};
		#endregion


		public Image[] Render(string file, params WaveFormRendererSettings[] settings)
		{
			using var reader = new AudioFileReader(file);
			var results = new Image[settings.Length];
			var bytesPerSample = reader.WaveFormat.BitsPerSample / 8;
			var samples = reader.Length / (bytesPerSample);
			for (int i = 0; i < settings.Length; i++)
			{
				reader.Position = 0;
				results[i] = RenderSamples(reader, samples, settings[i]);
			}
			return results;
		}

		private Image RenderSamples(AudioFileReader reader, long samples, WaveFormRendererSettings settings)
		{
			var samplesPerPixel = (int)(samples / settings.Width);
			var stepSize = settings.PixelsPerPeak + settings.SpacerPixels;
			PeakProvider.Init(reader, samplesPerPixel * stepSize);
			return RenderPeakProvider(PeakProvider, settings);
		}

		private Image RenderPeakProvider(IPeakProvider peakProvider, WaveFormRendererSettings settings)
		{
			if (settings.DecibelScale)
				peakProvider = new DecibelPeakProvider(peakProvider, 48);

			var b = new Bitmap(settings.Width, settings.TopHeight + settings.BottomHeight);
			if (settings.BackgroundColor == Color.Transparent)
				b.MakeTransparent();
			using var g = Graphics.FromImage(b);
			g.FillRectangle(settings.BackgroundBrush, 0, 0, b.Width, b.Height);
			var midPoint = settings.TopHeight;

			int x = 0;
			var currentPeak = peakProvider.GetNextPeak();
			while (x < settings.Width)
			{
				var nextPeak = peakProvider.GetNextPeak();

				for (int n = 0; n < settings.PixelsPerPeak; n++)
				{
					var lineHeight = settings.TopHeight * currentPeak.Max;
					g.DrawLine(settings.TopPeakPen, x, midPoint, x, midPoint - lineHeight);
					lineHeight = settings.BottomHeight * currentPeak.Min;
					g.DrawLine(settings.BottomPeakPen, x, midPoint, x, midPoint - lineHeight);
					x++;
				}

				for (int n = 0; n < settings.SpacerPixels; n++)
				{
					// spacer bars are always the lower of the 
					var max = Math.Min(currentPeak.Max, nextPeak.Max);
					var min = Math.Max(currentPeak.Min, nextPeak.Min);

					var lineHeight = settings.TopHeight * max;
					g.DrawLine(settings.TopSpacerPen, x, midPoint, x, midPoint - lineHeight);
					lineHeight = settings.BottomHeight * min;
					g.DrawLine(settings.BottomSpacerPen, x, midPoint, x, midPoint - lineHeight);
					x++;
				}
				currentPeak = nextPeak;
			}
			return b;
		}
	}
}
