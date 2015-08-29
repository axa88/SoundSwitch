/********************************************************************
* Copyright (C) 2015 Jeroen Pelgrims
* Copyright (C) 2015 Antoine Aflalo
* 
* This program is free software; you can redistribute it and/or
* modify it under the terms of the GNU General Public License
* as published by the Free Software Foundation; either version 2
* of the License, or (at your option) any later version.
* 
* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.
********************************************************************/

using System;
using System.Collections.Generic;
using AudioEndPointControllerWrapper;
using SoundSwitch.Framework;
using SoundSwitch.Framework.Updater;

namespace SoundSwitch.Model
{
    public interface IAppModel
    {
        #region Properties

        /// <summary>
        ///     The list of Playback device selected to be used for Switching default devices.
        /// </summary>
        HashSet<string> SelectedPlaybackDevicesList { get; }

        /// <summary>
        ///     An union between the Active <see cref="AudioDeviceWrapper" /> of Windows and <see cref="SelectedPlaybackDevicesList" />
        /// </summary>
        List<AudioDeviceWrapper> AvailablePlaybackDevices { get; }

        /// <summary>
        ///     If the Playback device need also to be set for Communications.
        /// </summary>
        bool SetCommunications { get; set; }

        /// <summary>
        ///     A string repsenting the set Keyboard shortcut for Switch default devices.
        /// </summary>
        string HotKeysString { get; }

        /// <summary>
        ///     If the application runs at windows startup
        /// </summary>
        bool RunAtStartup { get; set; }

        #endregion

        #region Events

        /// <summary>
        ///     When the selected list of device to switch from is changed (new device added or removed).
        /// </summary>
        event EventHandler<SoundSwitch.Model.AppModel.DeviceListChanged> SelectedPlaybackDeviceChanged;

        /// <summary>
        ///     If an exception happened in the <see cref="IAppModel" />
        /// </summary>
        event EventHandler<SoundSwitch.Model.AppModel.ExceptionEvent> ErrorTriggered;

        /// <summary>
        ///     The Default Playback device has been changed.
        /// </summary>
        event EventHandler<SoundSwitch.Model.AppModel.AudioChangeEvent> DefaultPlaybackDeviceChanged;

        /// <summary>
        ///     The update checker found a newer release than the installed version.
        /// </summary>
        event EventHandler<UpdateChecker.NewReleaseEvent> NewVersionReleased;

        #endregion

        #region Methods

        /// <summary>
        ///     Initialize the Main class with Updater and Hotkeys
        /// </summary>
        void InitializeMain();


        /// <summary>
        ///     Add a playback device into the Set.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>
        ///     true if the element is added to the <see cref="T:System.Collections.Generic.HashSet`1" /> object; false if
        ///     the element is already present.
        /// </returns>
        bool AddPlaybackDevice(string deviceName);

        /// <summary>
        ///     Remove a device from the Set.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>
        ///     true if the element is successfully found and removed; otherwise, false.  This method returns false if
        ///     <paramref name="deviceName" /> is not found in the <see cref="T:System.Collections.Generic.HashSet`1" /> object.
        /// </returns>
        bool RemovePlaybackDevice(string deviceName);

        /// <summary>
        ///     Sets the hotkey combination
        /// </summary>
        /// <param name="hotkeys"></param>
        void SetHotkeyCombination(HotKeys hotkeys);

        /// <summary>
        ///     Attempts to set active device to the specified name
        /// </summary>
        /// <param name="device"></param>
        bool SetActiveDevice(AudioDeviceWrapper device);

        /// <summary>
        ///     Cycles the active device to the next device. Returns true if succesfully switched (at least
        ///     as far as we can tell), returns false if could not successfully switch. Throws NoDevicesException
        ///     if there are no devices configured.
        /// </summary>
        bool CycleActiveDevice();

        #endregion
    }
}