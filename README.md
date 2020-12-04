# CheckoutPaymentGateway - ASP.NET Core 5

Validates payment requests, stores card information, forwards payment requests and accepts responses from the acquiring bank.

## Task Assumptions
-

## Technologies Used

### .Net 5

- I updated from ASP.Net Core 3.1 to ASP.Net 5 to solve imcompatablity issue with entityframworkcore 3.1.9 and some of the other nuget packages I had installed
    
### Swagger.UI

-   Used
    
### Autofac

-   Used
    
### Automapper

-   Used
    
### JWT

-   Used
    
### Serilog

-   Used
    
## Improvements

### Database

-   SQL Server was dockerised in order to keep the project all self-contained in a network.
    
-   In a live situation, it might not be an ideal solution to put SQL Server in a docker container.
    
-   For this project I have used the SA account to connect to the database, this is to save time setting up the project and usually, I would create separate application and user login credentials.
    
### Applications

-   Appsettings.json and the environment versions should not hold the connection settings to the database or at least have them encrypted.

-   Setting up a user to use windows auth would have been ideal.

-   Assigning the response codes in the response controller should have had a mapping from my enums using automapper rather than manually mapping the responses.

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
eyJoaXN0b3J5IjpbLTEyMzc1OTk2MjhdfQ==
-->