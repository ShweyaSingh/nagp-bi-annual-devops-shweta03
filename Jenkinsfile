pipeline {
    agent any

    environment {
        scannerHome = tool name : 'sonar_scanner_dotnet'
        username = 'shweta03'
        repository = 'shweyasingh/nagp-app-shweta03'
    }

    options {
        timestamps()
        timeout(time: 1, unit: 'HOURS')
        buildDiscarder(logRotator(daysToKeepStr: '5', numToKeepStr: '5'))
    }

    stages {
        stage('Nuget Restore') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Start Sonar') {
            steps {
                withSonarQubeEnv('Test_Sonar') {
                    bat "${scannerHome}\\SonarScanner.MSBuild.exe begin /k:sonar-${username} /n:sonar-${username} /v:1.0"
                }
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet clean'
                bat 'dotnet build -c Release'
            }
        }

        stage('Test Cases') {
            steps {
                bat 'dotnet test DevOpsAssignment.Tests\\DevOpsAssignment.Tests.csproj -l:trx;logFileName=TestResult.xml'
            }
            post {
                always {
                    xunit(
                    thresholds: [skipped(failureThreshold: '0'), failed(failureThreshold: '0')],
                    tools: [MSTest(pattern: 'DevOpsAssignment.Tests/TestResults/TestResult.xml')]
                    )
                }
            }
        }

        stage('Stop Sonar') {
            steps {
                withSonarQubeEnv('Test_Sonar') {
                    bat "${scannerHome}\\SonarScanner.MSBuild.exe end"
                }
            }
        }

        stage('Build Docker') {
            steps {
                bat "docker build -t i-${username}-master --no-cache -f Dockerfile ."

                bat "docker tag i-${username}-master ${repository}:${BUILD_NUMBER}"
                bat "docker tag i-${username}-master ${repository}:latest"
            }
        }

        stage('Push Docker') {
            steps {
                withDockerRegistry([credentialsId: 'DockerHub', url: '']) {
                    bat "docker push ${repository}:${BUILD_NUMBER}"
                    bat "docker push ${repository}:${BUILD_NUMBER}"
                }
            }
        }

        stage('Docker & Kubernetes deployment') {
            parallel {
                stage('Docker deployment') {
                    steps {
                        script {
                            env.docker_port = 7400

                            env.container_id = bat(script: "docker ps -qf name=c-${username}-master", returnStdout: true).trim().readLines().drop(1).join('')

                            if (env.container_id != '') {
                                bat "docker stop c-${username}-master && docker rm c-${username}-master"
                        } else {
                                echo 'No such container exist'
                            }

                            bat "docker run -d --name c-${username}-master -p ${docker_port}:80 i-${username}-master"
                        }
                    }
                }

                stage('Kubernetes deployment') {
                    steps {
                        bat 'kubectl apply -f k8/deployment.yaml'
                        bat 'kubectl apply -f k8/service.yaml'
                    }
                }
            }
        }
    }
}
