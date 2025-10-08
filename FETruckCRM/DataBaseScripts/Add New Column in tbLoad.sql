use FleetExperts
go

ALTER TABLE tbLoad
ADD 
[ShipperPaymentUrl] [varchar](500) NULL,
[MCNumber] [nvarchar](50) NULL,
	[MCRefNo] [nvarchar](50) NULL,
	[ReceipetUrl] [varchar](500) NULL,
	[InGateEntryEditby] [bigint] NULL,
	[inGateEntryDate] [datetime] NULL,
	[IsInGateEnrty] [bit] NULL,
	[CarrierInvoiceUrl] [varchar](500) NULL,
	[Carrierinvoiceuploadedby] [bigint] NULL,
	[PrePayment] [numeric](18, 2) NULL,
	CarrierInvoicePaymentMadeDoneBy bigint,
	CarrierInvoiceReceivedDateDoneBy bigint,
	LoadCarrierPaymentStatus int NULL,
	CarrierReferenceNo varchar(50),  
	[ShipperReferenceNo] [varchar](50) NULL,	
	ShipperPaymentUpdateBy [bigint] NULL,
	[IsInvoiceEmailSent] [bit] NULL,
	[LoadActivationDate] [datetime] NULL;