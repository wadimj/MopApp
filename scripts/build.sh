#!/bin/bash
source ../deploy-envs.sh

#provide environmental variables locally
if [ -e ../../secrets.sh ]
then
    source ../../secrets.sh
    echo 'Enter Server Environment (local or aws)'
    read inp
    if [ inp === "aws" ]
    then
        export S_ENV="$inp"
    else
        export S_ENV="aws"
    fi    
fi


#AWS_ACCOUNT_NUMBER={} set in private variable
export AWS_ECS_REPO_DOMAIN=$AWS_ACCOUNT_NUMBER.dkr.ecr.$AWS_DEFAULT_REGION.amazonaws.com

# Build process
docker build -t $IMAGE_NAME --build-arg env=$S_ENV --build-arg conn="$CONNECTION_STRING" ../WebApplication1/
docker tag $IMAGE_NAME $AWS_ECS_REPO_DOMAIN/$IMAGE_NAME:$IMAGE_VERSION