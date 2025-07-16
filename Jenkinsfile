pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                    checkout scm
            }
            
        }
        stage('publish backend') {
            steps {
                powershell """
                    dotnet publish -c Release -o ./publish
                """
            }
        }
    }
}