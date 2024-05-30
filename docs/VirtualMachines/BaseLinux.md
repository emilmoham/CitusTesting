This virtual machine serves as a template which can be cloned to create dedicated virtual machines for specific services. This allows us to skip boilerplate steps when creating new virtual machines. 

## VM Information/Features
* Virtual Hardware
	* 1 CPU Cores
	* 4 GB Memory
	* 30 GB Storage
	* 2 Network Interfaces
		1) NAT
		2) Host Only/Bridged Adapter
* Operating System
	* Debian 12
* Packages
	* avahi-daemon
	* sudo
	* vim
* Miscellaneous
	* IPv6 Disabled

## Usage
Using this template allows us to skip some boilerplate steps when creating a new virtual machine for some new service we would like to try.

To make use of the template follow these steps:

#### Clone the Template Machine
1) In Oracle VM VirtualBox Manager, right click the template and choose `clone`
2) Name and Path are set at your discretion
3) For **MAC Address Policy** choose `Generate new MAC addresses for all network adapters`
4) Choose **Next**
5) Select the `Full Clone` option then hit **Finish**

#### Connect to the Clone
1) Start the VM in VirtualBox
2) Open a terminal with an SSH client and connect with `ssh username@debian-base`
	* If for any reason the host name of the machine cannot be resolved, you can always just right click the box in the Oracle VM VirtualBox Manager window and choose show to bring up the virtual machine in a window.
3) When prompted for a password use `password`

#### Change the Machine Hostname
1) Start by changing the hostname configuration with: `sudo hostnamectl set-hostname [new_hostname]`
	* Where '\[new_hostname\]' is the name you want for the machine
2) Open the hosts file: `vim /etc/hosts`
3) replace all instances of `debian-base` with `[new_hostname]`
	* An easy way to do this in vim is to use `:1,6s/debian-base/[new_hostname]/g`
4) `:wq` to write and quit

#### Restart to Apply Changes
1) Use the command `sudo shutdown -r now` to apply the configuration changes

## Detailed Setup
If this box ever needs to be recreated, you can do it with the following instructions:

### Create a new Virtual Machine
1) Create a new machine with the following hardware settings:
	* 1 Cores
	* 4 GB Memory
	* 30 GB Storage
	* Network:
		* Adapter 1 - Enabled and attached to **NAT** (this should be done by default)
		* Adapter 2 - Enabled and attached to **Host-Only Adapter**
		* Adapter 3 - (Optional) Enabled and attached to **Bridged Adapter**
			* Only use this if you want to be able to connect the VM from other machines on the host network.

### Install Debian 12
* Power on the machine and choose graphical install 
* Default settings for localization should be fine
* When prompted for the **Primary network interface** choose `enp0s3`
* Set **hostname** to the name you want for the template (i.e. debian-base)
* Leave **domain** blank
* Set the root password to be `password`
* Create a first user `username` with a password `password`
* Partition disks
	* Choose **Guided - use entire disk**
	* SCSCI3 (0,0,0) (sda)... should be the only disk available
	* Choose **All files in one partition**
	* Choose **Finish partitioning and write changes to disk**
	* Choose **Yes** to write the changes to the disks
* Choose **No** when prompted to Scan extra installation media 
* Configure the package manager
	* Default settings are fine
	* Leave Http Proxy setting blank
* When prompted about additional software to install, choose only:
	* SSH Server
	* Standard System Utilities
* Install the GRUB boot loader
	* Choose **Yes** when prompted 
	* Choose the **/dev/sda** device
* Allow the machine to reboot to complete OS installation

### Add Packages 
Log into the machine and install the following packages.

#### Sudo
This package lets us perform admin operations without needing to log into the root account all the time. 

Installation Instructions:
```bash
# log into root for installation
su root

# Install the sudo package
apt install -y sudo

# Add the account to the sudoers group
sudo usermod -aG sudo username

# Return to base account
exit

# Log out to refresh privileges
exit
```

Log back into the account to test the changes with: 
```bash
sudo apt update
```
#### avahi-daemon
This package allows us to ping the machine by hostname rather than the machine IP
```bash
sudo apt install  avahi-daemon
```

#### Vim
Always nice to have a text editor that isn't nano. You can use emacs if you prefer.
```bash
sudo apt install vim
```

### Configuration Edits:
#### Enable the extra adapter interfaces
List all interfaces on the machine: 
```bash
ip a
```
You should see 3 entries. Take note of the  altname for the interfaces marked as `DOWN`. You should see two lines that say something along the lines of `altname enp0s8`

Open the network interfaces configuration in an editor:
```bash
sudo vim /etc/network/interfaces
```

Add the following lines to the end of the file:
```conf
# Host Only Adapter Or Bridged
auto enp0s8
allow-hotplug enp0s8
iface enp0s8 inet dhcp
netmask 255.255.255.0
```
Replace the instances of `enp0s8` with your interface's altname in the above snippet


Bring up the Host Only Adapter interface
```bash
sudo ifup enp0s8
```

Bring up the Bridged Adapter interface
```Shell
sudo ifup enp0s9
```

#### Disable IPv6
Open the system control config in an editor:
```bash
sudo vim /etc/sysctl.conf
```

Add the following lines to the end of the file
```conf
net.ipv6.conf.all.disable_ipv6 = 1
net.ipv6.conf.default.disable_ipv6 = 1
```

Restart machine to apply the change
```bash
sudo shutdown -r now 
```

Once the machine is fully rebooted, you should be able to ping it from the host.