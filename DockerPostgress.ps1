docker --version
docker ps
docker run --rm --name pg-docker -e POSTGRES_PASSWORD=docker -d -p 5432:5432 postgres