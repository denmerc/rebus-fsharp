namespace giraffe_rebus_api

module HttpHandlers =

    open Microsoft.AspNetCore.Http
    open Giraffe
    open giraffe_rebus_api.Models

    let handleGetHello =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let response = {
                    Text = "Hello world, from Giraffe!"
                }
                return! json response next ctx
            }

    open Rebus.Bus
    open MyBus.Commands

    let sayHello (name: string): HttpHandler =
        fun next ctx ->
            task {
                let bus = ctx.GetService<IBus>()
                bus.Send({Name = name}).Wait() |> ignore
                return! Successful.OK "Your message has been published." next ctx
            }
