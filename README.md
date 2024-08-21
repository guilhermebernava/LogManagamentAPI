# **Real-Time Log Processing with WebSockets and Elasticsearch**

Welcome to the **Real-Time Log Processing** project! This project leverages modern technologies to receive, process, and store logs efficiently in Elasticsearch using WebSockets and SignalR.

## **Introduction**

This project is designed to handle real-time logging, receiving logs over WebSockets, processing them with SignalR, and storing them in Elasticsearch using the NEST library. It provides a scalable solution for logging in distributed systems.

## **Features**

- **Real-Time Log Reception:** Logs are received in real-time through WebSockets.
- **Efficient Storage:** Logs are efficiently stored and indexed in Elasticsearch.
- **SignalR Integration:** Real-time updates and notifications are handled using SignalR.

## **Technologies**

- **.NET 8** - The latest version of the .NET platform for building high-performance applications.
- **WebSockets** - For real-time communication between the client and server.
- **SignalR** - A library for adding real-time web functionality to applications.
- **Elasticsearch** - A distributed, RESTful search and analytics engine.
- **NEST** - An Elasticsearch client for .NET, used to interact with Elasticsearch.

## **Getting Started**

### **Prerequisites**

Before you begin, ensure you have the following installed on your machine:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop)
- [Elasticsearch](https://www.elastic.co/elasticsearch/)

### **Installation**

1. **Clone the Repository**

   ```bash
   git clone https://github.com/yourusername/real-time-log-processing.git
   cd real-time-log-processing

  Install Dependencies

2. Use the .NET CLI to restore the project dependencies:

dotnet restore

Setup Elasticsearch

4. If you're using Docker, you can spin up an Elasticsearch container:
5. 
docker run -d --name elasticsearch -p 9200:9200 -e "discovery.type=single-node" elasticsearch:8.0.0

4. To run the application, use the following command:

dotnet run
