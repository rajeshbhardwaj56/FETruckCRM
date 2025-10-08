use FleetExperts
go
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ALTER proc [dbo].[updCarrierPaymentMadeStatus]      
(      
@LoadID bigint,      
@IsCarrierPaymentMade bit  ,    
@CarrierInvoiceReceivedDate varchar(100)  ,  
@LoggedinUserId bigint,
@CarrierReferenceNo varchar(50)
)      
AS      
BEGIN      
update tbLoad set IsCarrierPaymentMade=@IsCarrierPaymentMade      
,CarrierPaymentMadeDate=@CarrierInvoiceReceivedDate,CarrierInvoicePaymentMadeDoneBy=@LoggedinUserId,
CarrierReferenceNo=@CarrierReferenceNo   where LoadID=@LoadID      
      
select @@ROWCOUNT      
END    


