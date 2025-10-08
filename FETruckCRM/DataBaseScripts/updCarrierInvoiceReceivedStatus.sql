ALTER proc [dbo].[updCarrierInvoiceReceivedStatus]        
(        
@LoadID bigint,        
@IsCarrierInvoiceReceived bit,      
@CarrierInvoiceReceivedDate varchar(100),  
  
@carrierinvoiceurl varchar (500), 
@Carrierinvoiceuploadedby bigint,
  
@LoggedinUserId bigint    
)        
AS        
BEGIN        
update tbLoad set IsCarrierInvoiceReceived=@IsCarrierInvoiceReceived        
,CarrierInvoiceReceivedDate=@CarrierInvoiceReceivedDate     
,CarrierInvoiceUrl=@carrierinvoiceurl  
,Carrierinvoiceuploadedby=@Carrierinvoiceuploadedby    
,CarrierInvoiceReceivedDateDoneBy=@LoggedinUserId 
,LoadCarrierPaymentStatus = 2
where LoadID=@LoadID        
        
select @@ROWCOUNT        
END 


