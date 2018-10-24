FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet build
EXPOSE 22/tcp

ENV ASPNETCORE_URLS http://*:5000

ARG env=local
ENV SERVER $env

#Travis 'ARG conn'?
ARG conn="Server=db; Database=master; User=sa; Password=Morele69;"
ENV CONNECTION_STRING $conn

RUN chmod 755 ./entrypoint.sh
CMD ["./entrypoint.sh"]