# Rebus FSharp

This is an example showing how to configure and use [Rebus service bus](https://github.com/rebus-org/Rebus.ServiceProvider) bus in .NET Core using F# and [Giraffe](https://github.com/giraffe-fsharp/Giraffe#getting-started).  This is the code for the following blog article:

http://kearon.blogspot.com/2018/06/using-rebus-service-bus-from-f-with.html


# Giraffe's Readme

A [Giraffe](https://github.com/giraffe-fsharp/Giraffe) web application, which has been created via the `dotnet new giraffe` command.

## Build and test the application

### Windows

Run the `build.bat` script in order to restore, build and test (if you've selected to include tests) the application:

```
> ./build.bat
```

### Linux/macOS

Run the `build.sh` script in order to restore, build and test (if you've selected to include tests) the application:

```
$ ./build.sh
```

## Run the application

After a successful build you can start the web application by executing the following command in your terminal:

```
dotnet run src/giraffe_rebus_api
```

After the application has started visit [http://localhost:5000](http://localhost:5000) in your preferred browser.
