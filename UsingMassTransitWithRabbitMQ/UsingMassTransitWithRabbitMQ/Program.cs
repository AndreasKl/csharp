﻿using System;
using System.Threading;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Samples.MassTransit.SystemStatus;
using log4net;

namespace Samples.MassTransit
{
  public class Program
  {
    private static readonly ILog Log = LogManager.GetLogger( typeof( Program ) );

    public static void Main( string[] args )
    {
      Log.Info( "Starting mass transit producer." );

      var container = new WindsorContainer();
      container.Install( FromAssembly.This() );

      var systemStatusController = container.Resolve<SystemStatusController>();
      for( int i = 0; i < 25; i++ )
      {
        systemStatusController.PublishSystemStatus();
        Thread.Sleep( 500 );
      }

      Console.WriteLine( "Press any key to exit." );
      Console.ReadKey();

      container.Dispose();
    }
  }
}