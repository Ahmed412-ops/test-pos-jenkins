pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                    checkout scm
            }

        }
        stage('publish backend') {
            powershell """
                dotnet publish -c Release -o ./publish
            """
    }
}