module MyBus

open Microsoft.Extensions.DependencyInjection
open Rebus.Bus
open Rebus.Handlers
open Rebus.Routing.TypeBased
open Rebus.ServiceProvider
open Rebus.Transport.InMem
open Rebus.Transport.FileSystem
open Rebus.Retry.Simple

    module Commands =

        [<CLIMutable>]
        type SayHello = { Name: string }


    module Handlers =

        // Taken from Veikko Eeva's comment here:
        // https://fslang.uservoice.com/forums/245727-f-language/suggestions/6092853-async-waittask-for-non-generic-task
        let inline TaskUnitToTask(task: System.Threading.Tasks.Task<Unit>) =
            task :> System.Threading.Tasks.Task

        let inline RunAsRebusTask x =x |>  Async.StartAsTask |> TaskUnitToTask

        type SayHelloHandler () =
            interface IHandleMessages<Commands.SayHello> with
                member x.Handle(m: Commands.SayHello): System.Threading.Tasks.Task =
                    async {
                        printfn "Rebus has handled your message and says: 'Hello %s'!" m.Name
                    }
                    |> RunAsRebusTask


let configureRebusServices (services : IServiceCollection) =
    // Register handlers
    services.AutoRegisterHandlersFromAssemblyOf<Handlers.SayHelloHandler>() |> ignore

    // Configure the bus.
    let x = services.AddRebus (
                fun cfg ->
                    cfg
                        .Logging(fun l -> l.None())
                        .Transport(fun x -> x.UseFileSystem(@"c:\rebus", "my-queue"))
                        .Routing(fun r -> r.TypeBased().Map<Commands.SayHello>("my-queue") |> ignore)
                        .Options(fun x -> x.SimpleRetryStrategy(maxDeliveryAttempts = 1))
        )

    x