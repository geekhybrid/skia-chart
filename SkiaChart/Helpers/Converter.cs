﻿using SkiaChart.Exceptions;
using SkiaSharp;
using System;

namespace SkiaChart.Helpers {
    
    // A class reponsible for converting real world data into pixel scale equivalent
    internal class Converter {
        
        // Creates an instance of the converter class with given params
        internal Converter(SKRect rect, float xOffSet, float yOffSet) {
            if (rect==null) {
                throw new ChartAreaNotDefinedException("The SKRect object that defines the " +
                    "chart area can not be null");
            }
            Rect = rect;
            XOffset = xOffSet;
            YOffset = yOffSet;
        }

        //Converts the pixel scaled X value back into it's equivalent real value
        internal float XValueToRealScale(float pixelValue, float max, float min) {
            var pixelToUse = pixelValue - XOffset;
            var result = max - ((Rect.Width - pixelToUse) * (max - min) / Rect.Width);
            return (float) Math.Round(result, 2);
        }

        //Converts the pixel sceled Y value back into it's equivalent real value
        internal float YValueToRealScale(float pixelValue, float max, float min) {
            var pixelTouse = pixelValue - YOffset;
            double result = max - ((Rect.Height - pixelTouse) * (max - min) / Rect.Height);
            return (float) Math.Round(result, 2);
        }

        //Converts the real world X-Y values into it's equivalent pixel scale value
        internal SKPoint ToPixelScale(SKPoint point, float Xmax, float Xmin, float Ymax, float Ymin) {
            var result = new SKPoint {
                X = XOffset + ((point.X - Xmin) * Rect.Width / (Xmax - Xmin)),
                Y = (YOffset + (point.Y - Ymin) * Rect.Height / (Ymax - Ymin))
            };
            return result;
        }

        internal SKRect Rect { get; }
        internal float XOffset { get; }
        internal float YOffset { get; }
    }
}
