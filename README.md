# CheckoutPaymentGateway - ASP.NET Core 5

Validates payment requests, stores card information, forwards payment requests and accepts responses from the acquiring bank.

## Task Assumptions
- 

## Technologies Used

### .Net 5

- I updated from ASP.Net Core 3.1 to ASP.Net 5 to solve imcompatablity issue with entityframworkcore 3.1.9 and some of the other nuget packages I had installed
    
### Swagger.UI

-   Swagger.UI was used to create the API contracts and generate the endpoint shell. This also handled a lot of the endpoint documentation.
- The API client project is entirely swagger generated code with the exception of the added bearer token because it worked without having to hqand craft anything. I would have implemented it differently if I created it by hand.
	- A NUnit test project and a console app have been added to the client in order to test server endpoints and make load testing relatively simple.
    
### Autofac

- I chose to use Autofac for my Dependency Injection (DI) because I had the greater familiarity to the build in .netcore DI. In hindsite this was added complexity and perehaps the native DI would have been a better choise.
    
### Automapper

-   Automapper was configured to convert between the endpoint contracts to internal pocos and then to database objects. This was to simply code and remove potential duplicate mapping,
    
### JWT Authentication

-   Added to authenticate the calls to the server endpoint.
    
### Serilog

- Was used as the logging framework for the simple set up and that it integrates with elastic search. 

### Docker

- I used Docker to containerise all the server code into its own docker network.

### Prometheus

**NOTE:** This is currently not able to scrape the metrics when containerised. I am working to fix this.

-   Prometheus was chosen to gather the metrics because it was simple to set up and had a vareity of metrics to implement.
	- I have currently only set up a simple counter for the endpoints with the intention of adding more metrics to this in my own time to improve my understanding of the tool.
    
## Improvements

### Database

-   SQL Server was dockerised in order to keep the project all self-contained in a network.
    
-   In a live situation, it might not be an ideal solution to put SQL Server in a docker container.

-   For this project I have used the SA account to connect to the database, this is to save time setting up the project and usually, I would create separate application and user login credentials.
    
### Applications

-   Appsettings.json and the environment versions should not hold the connection settings to the database or at least have them encrypted.

-   Assigning the response codes in the response controller should have had a mapping from my enums using automapper rather than manually mapping the responses.

- I would have liked to have had code to match the supplied bankname to the bin on the card number but I felt that this was a stretch goal that I did not have time to implement.

- This project actually lends itself to an async implementation and if I were to plan this again I would have written the API this way.

- When adding a user or a card to the database I catch then throw the error. I am not happy with this implementation firstly I would prefer to handle the error then have a retry mechanism. 
	- I also think that the failure to add a user or card to the database is not a critical error in terms of the payment flow. 
	- However, this was something I should have queired in order to get a solid requirement

## Installation

### Checkout Payment Gateway

#### Use Command Line

1.  Navigate to the containing folder:
 > cd {Path}\CheckoutPaymentGatewayApi\Server\CheckoutPaymentGateway
2.  Build the solution:
	1.  Please note the command actually runs from the solution folder but needs to be in the project folder to get the docker file.
> docker build -f Dockerfile -t checkoutpaymentgateway ..
3.  Check the image has been created:
> docker images | more
4.  Create the docker container:
> docker-compose up -d
5.  Navigate to the Api:
> https://localhost:6001/Swagger
![](https://lh3.googleusercontent.com/NTS7R5SS6ZJVmJ4_rcNirj9_TO6PuP0IDJD9Oe5QXs_1Jd0wvyG7oaFBHZqubbDSEn99W0-c4oMpTS0YT-Ndj_r8mnR5dOJKk-I_KYmCCRdSbKj6QySk_A6E-EWhAhdZ2s98VQJU)

#### Connecting to the database

Server: 
> 127.0.0.1,1633 || localhost,1633 || paymentdb, 1633

Authentication:
> SQL Server Authentication

Login:
> SA

Password:
> %zYG614&
![](https://lh3.googleusercontent.com/EytPwg3J6sxCE5eXZ8iYXMK3tJ9TZNa9p5h0VDIQrAd5sKtpW5TG6IjLfREj9EpWG1uN_FU1vduUlBz0JueGoribhAYGQTYkGN87AwKWi-LTYMF4HcaqOGHUENMz87cX8PQPS1Px)

### Prometheus

#### Use Command Line

1.  Navigate to Prometheus for metrics:
> http://localhost:9090/graph

2. Select the metrics to show:
> Paymentgatewayapi_counter
![](https://lh3.googleusercontent.com/Z7684lLTYZ-T0BwMYpnp1mIsIvGb8aUQVsOD8deobd26L5at9EHlFfYwzgT61xGNN4Obql_NT1_wBCqMG76POzA8tw3tNN2M2aljy4zuJLpW-_7lWuImuwyIftBRxtx8F27p613O)

3.  Click Execute
    
### Grafana

#### Use Command Line
1.  Navigate to Grafana:
> http://localhost:3000/login
![](https://lh6.googleusercontent.com/m1SHRPuJKOT85S-IO0QNVfs_-GbpBUXiflVjhN7gy0zw7eJg25RZvRxPNaWzdOabQiHoMaeYOHV9HO48xusRNA0kyOnP6YzjZTN7a8qpAq91BYzcJ6abSvy3WAUw1lc1OadKaMqH)

2.  Log in:

Username: 
> admin

password: 
> P@ssw0rd

3.  Add Datasource
    
Click Add your first data sourse
![](https://lh3.googleusercontent.com/Ok5gWWhF8U0oNLdhjb7Z-hjC07j21FBX53_8O_FrScNCG28fhV_sO5TYp7QUZvN6mbTcVN1JfnZSwP0-55X3nNcohOi6-BwnK4NdQcqphZtqypIrouvRYW3Czas9CkPOzii89UkI)

Click Prometheus
![](https://lh4.googleusercontent.com/yl-cdODQ-Iui8HaLrKmPqsQ6WzRYzCQKYHh39A3UPQM9dnQiMl824ni5jUymfwc_ByStCEIlYZxTvubI8cgi4kxHXmAHavgX4_UYiicnefL0Eh9UrJFLKB9ARPDIamULShQ9hbRV)

Create a useful name i.e. checkout gateway metrics

Add the URL: 
> http://localhost:9090/

![](https://lh3.googleusercontent.com/9nb5c_OLFf6OyR-MR4CX1CwTYtxW52tNbd6HrNxFh1reH5yxq21w42RgobjncUDGNgpuzw_MsoGJc9MgFe4cqsEGqFasmdH_FQ23JpLB-hUIPzgD8q4Apa0x1Z4PJm3KZrpNon8x)

Click Save & Test
![](https://lh4.googleusercontent.com/dOae9Il5NXUitymEg_1Wn4vBWY6UT58sk1XUmEuyurOG9DOxTTINoOVUghgXRcz95lXgQFJteD3jnCcDAOiyqdBKPDEsVklf-Ixj51wd4HuTyvbP9_GnD_7R49S3ZkWscA5cjss3)

  
#### Add Dashboard
1.  Example dashboard imports:
Click the + on the left of the screen
![](https://lh5.googleusercontent.com/UVMcgiknn40bJttin6gKniAExnpUt0J9CuJbu5iWh_FXEa_Rmu8EyA-t_lSftKUONc9nMjaZW7Jcyt3FG3zyG_kbdiqA6-l5zO1U1enm1HKeGWY-80tH_PaJzm6DPP7jOBnpH2i3)

To load preset dashboards type the id:
> 10427

Or the id:
> 10915

![](https://lh6.googleusercontent.com/qCBdikg7fvjUh3Tk_y1cNiwlct_aGpQ0hKLt_3pmoAexyfiCNJRnVXDjBjkYd5_B-s1nNU_GD-lCJ5XWbqSEUTsM4EYW89hy5HQkq1YLpLpc1BBBcNcMutvwZpL3o318PwlY2X8T)

Click Load
Select the data source you created from the drop-down (above import)
Click Import
![](https://lh4.googleusercontent.com/5-kGNZ1ODOLT4M9l7_B_3rgsY_MgNJ1bBrVA8EJa_qcP9yLR6_nykgMwGwK1Ppd4fiEtVzW24UGdLaMqXymfpBhL040pKP542-sDIQ5jgANenCYr6UZ7OGftwPxvz3nMWjWc1S23)

A dashboard can also be built from scratch

### Kibana

1. Navigate to:
> http://
2. Click the hamburger in the top left
3. Click Discover
![](https://lh4.googleusercontent.com/c5kzP2p9WWn_ONsPpQKDTfbSSpPnL8WXipxdaet6j2BtqDAGGujPbPwT_xnDqTD1hs_Z9aI8HHTkj8PR5GIgmxWoM3-rabEiO8swFwfkjpZBzVBs2aJMOyYhu7dIx-7dIO-E7-5W)

4.  Add the index pattern below and click next step
> checkoutpaymentgateway-*

![](https://lh5.googleusercontent.com/agAZcoumX5Ltv35MwsSc2MG47sBr2onj_u5zrMAcBUO5dDYM2JLsXLdRYchZIeqH7wSXD6TfppKLudBlF_4f-fmtM5DMuXKm_QoBzehb6Arx8pRyVscZj9YnsWMsvsKIX7-CvbWC)

2.  Select @timestamp from the dropdown
3.  Click Create Index Pattern![](https://lh5.googleusercontent.com/HHVsqN-LU2_CVRu7mEV-0HWHSEiruQkkVl5DTRIl5N2VKHrO7VO67Kgeb6W3O9yynsl7IZfeGXRRbxxr84Oe-hu03B4c68sa3swwqAzBx6tgh1IGgIwKfirRMR-PReYi1MU6gRBi)

4.  Navigate to the Discover page again to see the logs


## Run

Bearer Token:
> eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJDaGVja291dFRlc3RDbGFpbSI6IkNoZWNrdXAiLCJleHAiOjE2OTg1Nzc1MjQsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjYwMDEiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo2MDAxIn0.mfR4GNH63EwgoOq8jlqYrD0XKVkaUF3XQztLjpLhRNo

  ### Swagger UI

1.  Navigate to:
	> https://localhost:6001/Swagger/index.html
2. Click Authorize   

![](https://lh4.googleusercontent.com/zKYH0PTrQTf9Vm9Zoz_7NhSOqeVIfvdKrhmY5FNcp1c7Y5Lxn7s88h5MIKyqC0mlQV85QUVtSw03EjxzNfYEZwQfqaim-6m90v3Nj4qxWTfv8pMuEY3VNocCcpfHmm6kcLGjTXi5)

3.  Add the bearer token into the box and click Authorize then close the popup
    ![](https://lh6.googleusercontent.com/sB7yE2ppJCX1uCwW-TPz_V0om4shO2cigjBgCs8EKB_bqpbDDLe9LZF5eqtT9CcYpPlbR4FUuMUeUGVcZYIAP7OG49vq50t_FxSxjohbxj7SpE6Dp65pt8c5t7FUiaG8Ck8bySmO)
![](https://lh6.googleusercontent.com/2p84xOmiUj_8oFZUk7Jm2NQC4QCy-LwlnhmEyUoj3mqLRHlfRFc33O7pk-hKM4RcB2KJn4BPQRKiv7utoPJsZofpm-Qzg-9XnJArAJRmGFxPFcR4Tss3JAxofxMMArGmZlWQvsNA)

NOTE: the padlocks on the API endpoints are now closed

![](https://lh5.googleusercontent.com/3foJMrWbuQydco2kazcvUVvrbShzBcuLaA6aXjhGTsGMzj_wqK6y72FJukKZEDfAdyd00PZhHgueAqHceIB8Q7qmVpdpSDzljqe7ViR4migND2WOJGSx-FtguI8k3eGTO4dk7hfl)

4.  Click the API Endpoint you wish to test
5.  Click Try it out
![](https://lh3.googleusercontent.com/OppgxJVcImtYfBW_D8aozkuv5qm2eh3anPiMWW9__yZ4MxInh4H91ShhZP2tNFBUkb5hzqzCsCGpEkxOiF-S4tcVRxc7_1pqJE-gIkzqFAHbefmZacSMQ01eXhQ9NjgXPMHj9s6a)

6.  Add a valid request (in this case any string value)
7.  Click Execute
![](https://lh4.googleusercontent.com/Sl3IXHwpRPz2Q7lxRaAimc-140EcYiifiM_aQhjd6Mx_joRMY_JbflkVvBE3Ll4M53R_fYNW6RfQiatt0czYkO1UtYd35kDhl1m09McboUUM_GNdVIU6iaXfr38DohRBbQqWLQaP)

8.  Check the response
![](https://lh3.googleusercontent.com/TtyXONzd6rxsU4GYXyn07y75A70xUYj35Tz0Gko4qamR0kC1_RJxiDFxwZ2_WMe_iQn38V5I_BuEw_rEojbv3C7rOjUr9gruJA34hfrGvujnb9rTLWK57CzkdlLHnhlcg-MeDxCl)

### Postman

1.  Click New in the top left
![](https://lh5.googleusercontent.com/Bv3uswG2yCHDY7n_cs-MazEjf-DxpYQVv8KJqM2oYBlM3LZBGNiNN_SxK1hExevREvrPzaJF75I_PAZ20IuKYp7fIrE5-jAmuL66nb_AJ9TD4CNxkaTWPMwW9u8zg_XecAk7eLXi)

2.  Click Request
![](https://lh3.googleusercontent.com/yELV8q2nzoDMb8rJFsSwjJni84KQIbP5eUd-6NhSOjPjk2m2xv5ZX8Sq13kOI0ZiDalWr0JDSFJNMjYcGxK-ZWQyVNh_UVVljgml9ofK0ICNnkuZxJLw5kDTz6sUYvRxPhR27Eff)

3.  Create a useful name
4.  Add it to a collection
5.  Save to collection
![](https://lh4.googleusercontent.com/Z0uMJ4ee3qQzJZ6vnlAYgx2b1dQtHGfom5XAofUHmGPzqCv2CNNUSJpiIS379MYeZTEEaMj7OIXeUg7S5mRJaoyMqO5yptalDNGQj1JY12HGmlGrtESSaY5mPl2fiToTSD3KT3JB)

6.  Enter the endpoint URL
![](https://lh3.googleusercontent.com/eTPh1Uj7fVJCLoKJazApUfRKd6LyhqM6Q_xfYOmO4BII5wh44YTu6Oy58Jr0NkTVGhdtj8Dq5lNuptZcOmuErL3gJbB4kFjCVbc-WI0sOG6HNzmfEogwr2I7GYKryibzdA0b2BBm)

7.  Click the Authorization tab
8.  Select Bearer Token from the drop down
9.  Enter the Bearer Token
![](https://lh6.googleusercontent.com/dqI1vvlQjCNCyB0t9vXHsPEKcNwEsE7b3ufBMT1vY13zLRP8Hw1fFOJeFimWDcbFRLEAvaNuSKumGtD8LdHkFmnNO2wrFpIzH9n_8JraOcPk3wriRwgEkb7sPuekcLl6ksGCtX5l)

10.  Click Send to receive the string Echo
![](https://lh5.googleusercontent.com/aVQYtwOpj48gmc2BODr6ygPNwi2W7vpOAsNbCnuhHaRAwp2JFHe8nosWz-h4LRS-sB1ONxHy6XS8A_PUa6aDzcaxACb6wTyHeIWcwN8tTwfrMPsk-8VpYRQou7FXDxNlQ7ekhwbd)

Edit the URL to:
> https://localhost:6001/checkoutpaymentgateway/Echo?echo=TestingTheApi

Pass your own string value to the echo endpoint

### Console App in the Client Api

**Note 1:** There is a test project called PaymentGatewayAPIClientTests that can be run to get the status of the Server Endpoint

**NOTE 2:** The console outputs all failed payments to a JSON file to the MyDocuments folder. To change the location go to the PaymentsSetup Class and edit the Line 79.

1.  Navigate to the Client folder
> {Path}\CheckoutPaymentGatewayApi\Server\CheckoutPaymentGateway
2.  Open the solution
3.  Right click on the solution and open properties
4.  Set GatewayLoadTest as the start-up project
![](https://lh5.googleusercontent.com/oOrNNScsPwYOZ7tPpq_eGO-_L0M0MI0bD8hoLH-20THSKVMSDjSY25--UW3m77SLiI0dYsm-7TFnzjvKS3eWGAQthBVtOPeY-Q_2Lea615Q7l5nS_9ilwh6C2pcdUV3mUHSiJk9k)

5.  Press F5 to run the project
6.  Type the number of payments you with to send to the API
![](https://lh3.googleusercontent.com/_cIW0me_ybH57Z-ZXadeobdx8FvwF02uXef1cEksF4FW7trGQ4VkBjdtk5Vdm4EDvdMdSM4HZay8OoeUeKHHoVgbZsX0h12KU5cWhaUOXH3xPaU4jRfWfZXfG8bEnIgkLnhjKKl4)

7.  The console with show the basic breakdown of:
	1.  Total Requests
	2.  Total passing requests
	3.  Total failing requests
	4.  A file path to a JSON output file of all the failed requests
<!--stackedit_data:
eyJoaXN0b3J5IjpbNDc3NDE4MjMzLDE1MTIwMzE4NDYsLTEwMT
k3OTA2NzYsMTYxOTU5ODE2MCwtNTIxNzU4NTE5LDIxMDY2Mjkx
NSwyMTIwODI1NjAxLDEyNzE1NzQzMjEsLTk2NTQxMTE3NywtMT
kyODA3MDk1OCwtNzE5NTQzMzc4LC0xMjM3NTk5NjI4XX0=
-->