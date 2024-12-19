# NServiceBus Demo

This solution demonstrates how to use NServiceBus to build a microservices architecture. It includes multiple endpoints that communicate with each other using NServiceBus messaging.

## Projects
------------

This solution consists of the following projects:

* `Demo.NServiceBus.ClientUI`: The entry point of the solution, containing the `Program.cs` file with the `Main` method.
* `Demo.NServiceBus.Message`: Contains the message definitions, including the `CreateOrder` command and the `OrderCreated` event.
* `Demo.NServiceBus.Shared`: Contains shared code used by multiple projects, including constants and a custom audit data behavior.
* `Demo.NServiceBus.ShippingManagement`: Contains the Shipping Management saga, which handles the `OrderCreated` event and the `BillingRecordCreated` event.
* `Demo.NServiceBus.OrderManagement`: Contains the Order Management endpoint, which starts the NServiceBus endpoint.
* `Demo.NServiceBus.BillingManagement`: Contains the Billing Management endpoint, which starts the NServiceBus endpoint.

## Getting Started

To run this solution, follow these steps:

1. Open the solution in Visual Studio.
2. Set the `Demo.NServiceBus.ClientUI` project as the startup project.
3. Press F5 to run the solution.
4. Follow the prompts in the console to place an order or quit.

## Architecture

This solution demonstrates a microservices architecture using NServiceBus. Each endpoint is a separate process that communicates with other endpoints using NServiceBus messaging.

The `Demo.NServiceBus.ClientUI` project contains the entry point of the solution, which allows the user to interact with the system.

The `Demo.NServiceBus.Message` project contains the message definitions, including the `CreateOrder` command and the `OrderCreated` event.

The `Demo.NServiceBus.Shared` project contains shared code used by multiple projects, including constants and a custom audit data behavior.

The `Demo.NServiceBus.ShippingManagement` project contains the Shipping Management saga, which handles the `OrderCreated` event and the `BillingRecordCreated` event.

The `Demo.NServiceBus.OrderManagement` project contains the Order Management endpoint, which starts the NServiceBus endpoint.

The `Demo.NServiceBus.BillingManagement` project contains the Billing Management endpoint, which starts the NServiceBus endpoint.

## NServiceBus Configuration

Each endpoint is configured to use NServiceBus messaging. The `Program.cs` file in each endpoint project contains the code to start the NServiceBus endpoint.

## Acknowledgments

This solution was built using NServiceBus, a popular open-source messaging library for .NET.