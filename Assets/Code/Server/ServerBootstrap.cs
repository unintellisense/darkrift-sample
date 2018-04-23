using Open.Nat;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ServerBootstrap : SingletonBehaviour {

    public int internalPort = 20000;

    public int externalPortRangeStart = 20000;
    public int externalPortRangeEnd = 25000;

    async Task Start() {
        Debug.Log($"{nameof(ServerBootstrap)} starting.");
        try {
            await StartServer();
        }
        catch (Exception e) {
            Debug.Log("failed to start server.");
            Debug.Log(e);
            return;
        }
        Debug.Log("Server started.");
    }

    private async Task StartServer() {
        int externalPort = await this.GetNatPunchthroughPort(5000);
    }

    private async Task<int> GetNatPunchthroughPort(int millisecondsToTry) {
        NatDiscoverer discoverer = new NatDiscoverer();

        CancellationTokenSource cts = new CancellationTokenSource(millisecondsToTry);
        NatDevice device = await discoverer.DiscoverDeviceAsync(PortMapper.Upnp | PortMapper.Pmp, cts);

        int externalPort = this.externalPortRangeStart;

        while (externalPort <= this.externalPortRangeEnd) {
            try {
                await device.CreatePortMapAsync(new Mapping(Protocol.Tcp, this.internalPort, externalPort, "Main server port"));
                return externalPort;
            }
            catch (MappingException e) {
                externalPort++;
            }
        }
        throw new Exception("Failed to punchthrough any ports.");
    }
}

