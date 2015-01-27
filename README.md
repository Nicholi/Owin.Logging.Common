# Owin.Logging.Common
Common.Logging factory implementation for OWIN Logging

## Using
Configure the underlying Commong.Logging Factory Adapter as usual.

Then to utilize Common.Logging in Owin, use extension methods:

```C#
using Owin.Logging.Common;

    public class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCommonLogging();
        }
    }
```

Overloaded methods are available to control the transform of TraceEventType to levels in Common.Logging.ILog

Default transform is:

| TraceEventType	| NLog Loglevel |
|-----------------|---------------|
| Critical        | Fatal			  	|
| Error			    	| Error 		  	|
| Warning			  	| Warn 		  		|
| Information		  | Info 			  	|
| Verbose			  	| Trace 	  		|
| Start				  	| Debug 		  	|
| Stop				  	| Debug 		  	|
| Suspend			  	| Debug 		  	|
| Resume			  	| Debug 		  	|
| Transfer			  | Debug 		  	|
