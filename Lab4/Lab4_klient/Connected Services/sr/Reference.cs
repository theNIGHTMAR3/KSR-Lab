﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lab4_klient.sr {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Wyjatek7", Namespace="http://schemas.datacontract.org/2004/07/Lab4_server")]
    [System.SerializableAttribute()]
    public partial class Wyjatek7 : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int BField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OpisField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string A {
            get {
                return this.AField;
            }
            set {
                if ((object.ReferenceEquals(this.AField, value) != true)) {
                    this.AField = value;
                    this.RaisePropertyChanged("A");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int B {
            get {
                return this.BField;
            }
            set {
                if ((this.BField.Equals(value) != true)) {
                    this.BField = value;
                    this.RaisePropertyChanged("B");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Opis {
            get {
                return this.OpisField;
            }
            set {
                if ((object.ReferenceEquals(this.OpisField, value) != true)) {
                    this.OpisField = value;
                    this.RaisePropertyChanged("Opis");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="sr.IZadanie7")]
    public interface IZadanie7 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IZadanie7/RzucWyjatek7", ReplyAction="http://tempuri.org/IZadanie7/RzucWyjatek7Response")]
        [System.ServiceModel.FaultContractAttribute(typeof(Lab4_klient.sr.Wyjatek7), Action="http://tempuri.org/IZadanie7/RzucWyjatek7Wyjatek7Fault", Name="Wyjatek7", Namespace="http://schemas.datacontract.org/2004/07/Lab4_server")]
        void RzucWyjatek7(string a, int b);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IZadanie7/RzucWyjatek7", ReplyAction="http://tempuri.org/IZadanie7/RzucWyjatek7Response")]
        System.Threading.Tasks.Task RzucWyjatek7Async(string a, int b);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IZadanie7Channel : Lab4_klient.sr.IZadanie7, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Zadanie7Client : System.ServiceModel.ClientBase<Lab4_klient.sr.IZadanie7>, Lab4_klient.sr.IZadanie7 {
        
        public Zadanie7Client() {
        }
        
        public Zadanie7Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Zadanie7Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Zadanie7Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Zadanie7Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void RzucWyjatek7(string a, int b) {
            base.Channel.RzucWyjatek7(a, b);
        }
        
        public System.Threading.Tasks.Task RzucWyjatek7Async(string a, int b) {
            return base.Channel.RzucWyjatek7Async(a, b);
        }
    }
}
