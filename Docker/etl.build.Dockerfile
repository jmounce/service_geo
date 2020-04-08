#---------------
# Build Image
#---------------
FROM python:3.7.7

WORKDIR /airflow

# airflow needs a home, ~/airflow is the default,
# but you can lay foundation somewhere else if you prefer
# (optional)
RUN export AIRFLOW_HOME=~/airflow

# install from pypi using pip
RUN pip install apache-airflow

# initialize the database
RUN airflow initdb

# start the web server, default port is 8080
CMD airflow webserver -p 8080 & \
	&& airflow scheduler &
