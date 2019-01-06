using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using GalaSoft.MvvmLight.Command;

namespace WPF
{
    public class MouseButtonEventArgsToPointConverter: IEventArgsConverter
        {
            public object Convert(object value, object parameter)
            {
                var args = (MouseButtonEventArgs) value;
                var element = (FrameworkElement) parameter;

                var point = args.GetPosition(element);

                return point;
            }

            public object ConvertBack(object value, object parameter)
            {
                return null;
            }
        }
    }

