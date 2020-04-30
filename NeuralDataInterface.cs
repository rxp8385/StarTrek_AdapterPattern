using System;
using System.Threading;

namespace DesignPatterns.GangOfFour.Structural.Adapter
{
    /// <summary>
    /// Adapter Design Pattern.
    /// 
    /// Definition: The Adapter Design Pattern converts the interface 
    ///             of a class into another interface clients expect.
    ///             This enables classes to work together that wouldn't
    ///             be able to otherwise due to incompatible interfaces.
    /// 
    /// The Adapter pattern is used primarily to allow incompatible services
    /// to work together using a common interface.  For example, the adapter
    /// pattern can be used to allow an existing system to call methods from
    /// a 3rd party API.  The adapter class can be used as a "go-between" class
    /// that enables the existing system to access the 3rd party API's methods.
    /// 
    /// The example below uses the Adapter Pattern to allow Commander Data 
    /// from "The Next Generation" Star Trek Series to communicate with an incompatible
    /// neural interface created by the Tholian species...
    /// </summary>
    public class Program
    {
        
        static void Main()
        {
            // Non-adapted alien neural interface
            var tholianNeuralInterface = new TholianNeuralInterfaceClass();
            //tholianNeuralInterface.Connect();

            // Uses the adapter class, AdaptedDataNeuralInterface, to enable communication between 
            // Commander Data and the incompatible, Tholian neural interface.
            var cerebralCortexPatch = new AdaptedDataNeuralInterface(InterfaceComponent.CerebralCortexPatch);
            cerebralCortexPatch.Connect();

        
            var temporalInterfacePatch = new AdaptedDataNeuralInterface(InterfaceComponent.TemporalInterfacePatch);
            temporalInterfacePatch.Connect();

            
            var frontalInterfacePatch = new AdaptedDataNeuralInterface(InterfaceComponent.FrontalInterfacePatch);
            frontalInterfacePatch.Connect();

            // User input ends program
            Console.ReadKey();
        }
    }

    /// <summary>
    /// The 'Target' class
    /// 
    /// In the Adapter pattern, the 'Target' class represents the incompatible interface
    /// that we need to use.
    /// In our example, this class is a neural interface used by the Tholian species.
    /// Commander Data's neural interface, ExistingDataNeuralInterfaceClass, is not compatible with the Tholian interface,
    /// so we have created an adapter class, AdaptedDataNeuralInterface, to enable information sharing.
    /// 
    /// </summary>
    class TholianNeuralInterfaceClass
    {
        public InterfaceComponent TholianNeuralInterfacePatch { get; protected set; }
        public double TholianDataTransferRate { get; protected set; }
        public TimeSpan TholianConnectionDuration { get; protected set; }
        public double AvgTholianPacketsSent { get; protected set; }
        public string TholianMessageHeader { get; protected set; }

        public virtual void Connect()
        {
            Console.WriteLine("\nInitializing Tholian Neural Interface Connection Parameters , please wait------ ");

            Thread.Sleep(5000);

            Console.WriteLine("\nParameters initialized, loading neural pathway interfaces, please wait------ ");

            Thread.Sleep(7000);

            Console.WriteLine("\nPathway interfaces loaded; waiting for connection from client neural interface----- ");



        }
    }

    /// <summary>
    /// The 'Adapter' class
    /// This is an implementation of the adapter class, AdaptedDataNeuralInterface,
    /// and in our example the adapter enables Commander Data to communicate
    /// with the Tholian neural interface.
    /// 
    /// </summary>
    /// 


    // Here we use the Adapter Pattern to create an adapter class (AdaptedDataNeuralInterface)
    // that inherits from the incompatible class (TholianNeuralInterfaceClass).
    class AdaptedDataNeuralInterface : TholianNeuralInterfaceClass
    {
        //Create an object using the class that needs to be adapted...
        ExistingDataNeuralInterfaceClass existingDataNeuralInterfaceObject;

 
        // In the constructor, set the TholianNeuralInterfacePatch property 
        // (inherited from the TholianNeuralInterfaceClass).
        //  We use the InterfaceComponent object that is passed in as a parameter
        //  to the constructor to set the property.
        //
        // The InterfaceComponent types are:
        //      CerebralCortexPatch,
        //      TemporalInterfacePatch,
        //      FrontalInterfacePatch
        public AdaptedDataNeuralInterface(InterfaceComponent neuralInterfacePatch)
        {

            TholianNeuralInterfacePatch = neuralInterfacePatch;

            
            existingDataNeuralInterfaceObject = new ExistingDataNeuralInterfaceClass();
        }


    

       
        public override void Connect()
        {
            DateTime connectionStartTime = DateTime.Now;
            DateTime connectionEndTime;
            

            // First, we call the Connect(...) method from the base class (TholianNeuralInterfaceClass)
            // to initialize the incompatible, TholianNeuralInterfaceClass connection parameters.

            base.Connect();

            // Next, We add new implementation details to use different "patches" to set the properties
            // of the incompatible class (TholianNeuralInterfaceClass) using values retrieved from
            // the unadapted class (ExistingDataNeuralInterfaceClass)

            // Here, We use the legacy class method, "GetPatchTransferRate", to assign a value
            // to the incompatible property "TholianDataTransferRate"...
            TholianDataTransferRate = existingDataNeuralInterfaceObject.GetPatchTransferRate(TholianNeuralInterfacePatch);
             
            // We do the same thing to the other properties...
            AvgTholianPacketsSent = existingDataNeuralInterfaceObject.GetPacketsSentRate(TholianNeuralInterfacePatch, State.ActiveConnectionEnabled);
            TholianMessageHeader = existingDataNeuralInterfaceObject.GetMessageHeader(TholianNeuralInterfacePatch, State.ActiveConnectionEnabled);

            Console.WriteLine("\nConnecting to Tholian Neural Interface using: " + TholianNeuralInterfacePatch + ".  Please wait...");
            Thread.Sleep(3000);
            
            Console.WriteLine("\nConnection established.  Data Transfer Rate: " + TholianDataTransferRate.ToString() + " petabytes per nanosecond.");

            Console.WriteLine("\nReceiving packets from Tholian " + TholianNeuralInterfacePatch + "... please wait...");
            Thread.Sleep(6000);

            Console.WriteLine("\nPackets received.   Avg Packets Received: " + AvgTholianPacketsSent.ToString() + " petabytes per nanosecond.");

            connectionEndTime = DateTime.Now;

            TholianConnectionDuration = connectionEndTime.Subtract(connectionStartTime);
            Console.WriteLine("\nConnection Duration : " + TholianConnectionDuration.ToString() + "\nSession Ended...\nConnection Terminated.");
           
        }
    }

    /// <summary>
    /// The 'Adaptee' class
    /// In our example, this is Commander Data's legacy neural interface.
    /// The Adapter class, AdaptedDataNeuralInterface, uses this legacy class
    /// and 'adapts' it to enable direct communication between Commander Data
    /// and the Tholian neural interface.
    ///
    /// </summary>
    class ExistingDataNeuralInterfaceClass
    {
        
        public double GetPacketsSentRate(InterfaceComponent interfaceComponent, State state)
        {
           
            if (state == State.ActiveConnectionNotEnabled)
            {
                switch (interfaceComponent)
                {
                    case InterfaceComponent.CerebralCortexPatch: return 0.0;
                    case InterfaceComponent.TemporalInterfacePatch: return 0.0;
                    case InterfaceComponent.FrontalInterfacePatch: return 0.0;
                    default: return 0;
                }
            }
            // ActiveConnectionEnabled Point
            else
            {
                switch (interfaceComponent)
                {
                    case InterfaceComponent.CerebralCortexPatch: return 512.88;
                    case InterfaceComponent.TemporalInterfacePatch: return 726.91;
                    case InterfaceComponent.FrontalInterfacePatch: return 100.3;
                    default: return 0;
                }
            }
        }

        public string GetPatchType(InterfaceComponent interfaceComponent)
        {
            switch (interfaceComponent)
            {
                case InterfaceComponent.CerebralCortexPatch: return "Cerebral Cortex Patch";
                case InterfaceComponent.TemporalInterfacePatch: return "Temporal Interface Patch";
                case InterfaceComponent.FrontalInterfacePatch: return "Frontal Interface Patch";
                default: return "Either no patch was specified or the patch type is undefined";
            }
        }

        public string GetMessageHeader(InterfaceComponent interfaceComponent, State state)
        {
            if (state == State.ActiveConnectionEnabled)
            {
                switch (interfaceComponent)
                {
                    case InterfaceComponent.CerebralCortexPatch: return this.GetType().ToString() + "Cerebral Cortex Patch Header";
                    case InterfaceComponent.TemporalInterfacePatch: return "Temporal Interface Patch Header";
                    case InterfaceComponent.FrontalInterfacePatch: return "Frontal Interface Patch Header";
                    default: return "Either no header was specified or the header is undefined";
                }

            }

            else
                return "No active connection established";
            
        }

        public double GetPatchTransferRate(InterfaceComponent interfaceComponent)
        {
            switch (interfaceComponent)
            {
                case InterfaceComponent.CerebralCortexPatch: return 2458.33;
                case InterfaceComponent.TemporalInterfacePatch: return 999.878;
                case InterfaceComponent.FrontalInterfacePatch: return 698.336;
            }
            return 0d;
        }

        //public float GetConnectionDuration(InterfaceComponent interfaceComponent)
        //{
        //    switch (interfaceComponent)
        //    {
        //        case InterfaceComponent.CerebralCortexPatch: return 6000.33f;
        //        case InterfaceComponent.TemporalInterfacePatch: return 9345.878f;
        //        case InterfaceComponent.FrontalInterfacePatch: return 9589.336f;
        //    }
        //    return 0f;
        //}
    }


    /// <summary>
    /// InterfaceComponent enumeration
    /// </summary>
    public enum InterfaceComponent
    {
        CerebralCortexPatch,
        TemporalInterfacePatch,
        FrontalInterfacePatch
    }

    /// <summary>
    /// State enumeration
    /// </summary>
    public enum State
    {
        ActiveConnectionEnabled,
        ActiveConnectionNotEnabled
    }
}

