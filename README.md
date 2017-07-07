# OpenX12
X12 Document Parser

### Why (also orgins, also soapbox...)
The OpenX12 project was originally developed between 2005-2006 as a quick way to process an X12 document.  At that time, most EDI tools were black box applications with complex intergration points and limited or no debugging capability.  When dealing with mission critial data, the development tool chain should provide full confidence that integration points are working as intended.  In addition, the mapping software is complex abstration of technology at business level, not a technology level.  Consequently, decisions for endpoints are made around user comfort at that level which often means using legacy endpoints like flat files and relational databases.  These decisions typically compromise on speed of I/O or data compromises to reduce file sizes.  All of that leads to increased operational and infrastucture costs due to licensing and increased specialization.

#### Use case 1: Provider networks
When I first created OpenX12, I had transitioned from cost-containment and clearing house TPA work to payer/plan health insurance processing of 4010 837 (claims) transactions.  I was tasked with scraping out the medical provider information for another department to see if they were part of our network and if not have enough information to contact them and try to get them to join.  OpenX12 was in its infancy at this stage but this task propelled it's development.  A custom map was overkill for this since transactions could not be sent twice through the mapping software and adding a second file with already referenced data was very complicated. It was also very slow to spin up because the software used MSAccess under the hood and required schema to be build and torn down.  .NET & OpenX12 to the rescue and this process was completed in a few days

#### Use case 2: Unbundling transactions
Bundled transactions are where more than one functional group exists in an interchange or more than one transaction set exists in a functional group.  Bundling is great when you have a Value Added Network (VAN) charging by the kilo-char since it reduces the byte size but forces batch integrity when a single transaction fails a downstream process.  Unbundling reduces the interchange-functional group-transaction set relatiotionship from 1:M:M to 1:1:1 allowing single transactions to drop out while 1 transaction gets reworked.  Since OpenX12 follows an object hierarchy, looping over the Interchange, Functional Groups and Transaction Sets is trivial. Unbundling has since been added natively to OpenX12.

#### Use case 3: National Provider Id (NPI)
Similar to the Provider Network use case, I was tasked with reading provider information out a transaction and calling a web service on a service bus that would provide a ranked list of NPIs, then inject that NPI as a segment on the transaction.  The mapping software relied on COM+ object to achieve this task.  In addition, the traditioanl mapping time processing 10,000 transactions took 30 minutes to achieve this task.  With OpenX12, this process was a breeze and the same activity could processes 10,0000 transactions in 15 seconds!

### Where it fits
OpenX12 useful after compliance checking and transaction auditing has been performed but in spaces where mapping software is burdensome. Since OpenX12 is a .Net library, you have all the benefits of .NET while also being able to read, annotate and write X12.

### What it isn't
OpenX12 is not a drop in replacement for mapping software, broad document serialization or compliance checking.  Tools like Gentan, Trizetto Gateway, Biztalk, Mule, or Tibco (to name a few) are great at the broad concept of EDI will always have a place in standard business flows. The intent of OpenX12 was never to replace those tools, but rather augment in areas where those tools fail to deliver.

### What it is
The OpenX12 library is a basic parsing tool to read an X12 document and seperate it into its hierachical components. Its very similar to an XML parser in that its just maintaining stucture but rather in the X12 domain. It abstracts away some of the complexities of the X12 structure, rules and symantic making it easier to achieve business value.  Your technology stack should add value and not be a neccessary evil (cost center).

### Why the reboot?
After nearly a decade away from EDI, the .NET ecosystem has matured greatly and industry development practices have changed for the better.  [The old project](https://sourceforge.net/projects/openx12/?source=directory) was also written when .NET 2.0 was new and 4010 was still king.

The reboot leverages a lot of the existing code but corrects common pitfals of the library. It exploits newer technology and addresses minor changes for 5010.  Plus there are unit tests confirming that what was intended was actually done.  A big difference from the prior version is that mapping will no longer be embedded as a part of the library but rather as an add-on to separate concerns and allow mapping to better mutate into real business objects.
