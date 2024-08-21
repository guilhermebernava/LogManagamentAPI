CRIAR O ELASTICSEARCH PELO DOCKER

docker run --name es01 --net elastic -p 9200:9200 -it docker.elastic.co/elasticsearch/elasticsearch:8.9.0