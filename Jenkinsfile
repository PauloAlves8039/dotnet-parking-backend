pipeline {
    agent any

    environment {
        IMAGE_NAME = "dotnet-parking-api"
        CONTAINER_NAME = "parking-api-container"
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/PauloAlves8039/dotnet-parking-backend.git'
            }
        }

        stage('Setup .NET') {
            steps {
                script {
                    sh 'dotnet --version'
                }
            }
        }

        stage('Restore Dependencies') {
            steps {
                script {
                    sh 'dotnet restore'
                }
            }
        }

        stage('Build') {
            steps {
                script {
                    sh 'dotnet build --configuration Release'
                }
            }
        }

        stage('Run Tests') {
            steps {
                script {
                    sh 'dotnet test --no-build --verbosity normal'
                }
            }
        }

        stage('Docker Build') {
            steps {
                script {
                    sh 'docker build -t ${IMAGE_NAME}:latest .'
                }
            }
        }

        stage('Docker Run') {
            steps {
                script {
                    sh 'docker stop ${CONTAINER_NAME} || true'
                    sh 'docker rm ${CONTAINER_NAME} || true'
                    sh 'docker run -d -p 5000:5000 --name ${CONTAINER_NAME} ${IMAGE_NAME}:latest'
                }
            }
        }
    }
}
