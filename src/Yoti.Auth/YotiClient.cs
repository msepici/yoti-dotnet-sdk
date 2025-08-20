﻿using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto;
using Yoti.Auth.Aml;
using Yoti.Auth.ShareUrl;

namespace Yoti.Auth
{
    public class YotiClient
    {
        private readonly string _sdkId;
        private readonly AsymmetricCipherKeyPair _keyPair;
        private readonly YotiClientEngine _yotiClientEngine;
        internal Uri ApiUri { get; private set; }

    /// <summary>
    /// Request attribute with a fallback to another attribute.
    /// </summary>
    /// <param name="primaryAttribute">The primary attribute to request.</param>
    /// <param name="fallbackAttribute">The attribute to use as a fallback.</param>
    /// <returns>A structured response indicating the result.</returns>
    public AttributeResult RequestAttributeWithFallback(string primaryAttribute, string fallbackAttribute)
    {
        // Logic to request primary attribute
        bool primarySuccess = RequestAttribute(primaryAttribute, out var primaryResult);
        if (primarySuccess)
        {
            return new AttributeResult(primaryResult, fallbackUsed: false);
        }

        // Fallback to secondary attribute
        bool fallbackSuccess = RequestAttribute(fallbackAttribute, out var fallbackResult);
        if (fallbackSuccess)
        {
            return new AttributeResult(fallbackResult, fallbackUsed: true);
        }

        // Return failure response
        return new AttributeResult(fallbackUsed: false);
    }

    private bool RequestAttribute(string attributeName, out object result)
    {
        // Placeholder logic for requesting attribute
        result = null;
        return false; // Assume failure for now
    }
        /// <summary>
        /// Create a <see cref="YotiClient"/>
        /// </summary>
        /// <param name="sdkId">The client SDK ID provided on the Yoti Hub.</param>
        /// <param name="privateKeyStream">
        /// The private key file provided on the Yoti Hub as a <see cref="StreamReader"/>.
        /// </param>
        public YotiClient(string sdkId, StreamReader privateKeyStream)
            : this(new HttpClient(), sdkId, CryptoEngine.LoadRsaKey(privateKeyStream))
        {
        }

        /// <summary>
        /// Create a <see cref="YotiClient"/> with a specified <see cref="HttpClient"/>
        /// </summary>
        /// <param name="httpClient">Allows the specification of a HttpClient</param>
        /// <param name="sdkId">The client SDK ID provided on the Yoti Hub.</param>
        /// <param name="privateKeyStream">
        /// The private key file provided on the Yoti Hub as a <see cref="StreamReader"/>.
        /// </param>
        public YotiClient(HttpClient httpClient, string sdkId, StreamReader privateKeyStream)
            : this(httpClient, sdkId, CryptoEngine.LoadRsaKey(privateKeyStream))
        {
        }

        /// <summary>
        /// Create a <see cref="YotiClient"/> with a specified <see cref="HttpClient"/>
        /// </summary>
        /// <param name="httpClient">Allows the specification of a HttpClient</param>
        /// <param name="sdkId">The client SDK ID provided on the Yoti Hub.</param>
        /// <param name="keyPair">The key pair from the Yoti Hub.</param>
        public YotiClient(HttpClient httpClient, string sdkId, AsymmetricCipherKeyPair keyPair)
        {
            Validation.NotNullOrEmpty(sdkId, nameof(sdkId));
            Validation.NotNull(keyPair, nameof(keyPair));

            _sdkId = sdkId;
            _keyPair = keyPair;

            SetYotiApiUri();

            _yotiClientEngine = new YotiClientEngine(httpClient);
        }

        /// <summary>
        /// Request an <see cref="ActivityDetails"/> using the encrypted token provided by yoti
        /// during the login process.
        /// </summary>
        /// <param name="encryptedToken">The encrypted returned by Yoti after successfully authenticating.</param>
        /// <returns>The account details of the logged in user as a <see cref="ActivityDetails"/>.</returns>
        public ActivityDetails GetActivityDetails(string encryptedToken)
        {
            Task<ActivityDetails> task = Task.Run(async () => await GetActivityDetailsAsync(encryptedToken).ConfigureAwait(false));

            return task.Result;
        }

        /// <summary>
        /// Asynchronously request a <see cref="ActivityDetails"/> using the encrypted token provided
        /// by yoti during the login process.
        /// </summary>
        /// <param name="encryptedToken">The encrypted returned by Yoti after successfully authenticating.</param>
        /// <returns>The account details of the logged in user as a <see cref="ActivityDetails"/>.</returns>
        public async Task<ActivityDetails> GetActivityDetailsAsync(string encryptedToken)
        {
            return await _yotiClientEngine.GetActivityDetailsAsync(encryptedToken, _sdkId, _keyPair, ApiUri).ConfigureAwait(false);
        }

        /// <summary>
        /// Request an <see cref="AmlResult"/> using an individual's name and address.
        /// </summary>
        /// <param name="amlProfile">An individual's name and address.</param>
        /// <returns>The result of the AML check in the form of a <see cref="AmlResult"/>.</returns>
        public AmlResult PerformAmlCheck(IAmlProfile amlProfile)
        {
            Task<AmlResult> task = Task.Run(async () => await PerformAmlCheckAsync(amlProfile).ConfigureAwait(false));

            return task.Result;
        }

        /// <summary>
        /// Asynchronously request a <see cref="AmlResult"/> using an individual's name and address.
        /// </summary>
        /// <param name="amlProfile">An individual's name and address.</param>
        /// <returns>The result of the AML check in the form of a <see cref="AmlResult"/>.</returns>
        public async Task<AmlResult> PerformAmlCheckAsync(IAmlProfile amlProfile)
        {
            return await _yotiClientEngine.PerformAmlCheckAsync(_sdkId, _keyPair, ApiUri, amlProfile).ConfigureAwait(false);
        }

        /// <summary>
        /// Initiate a sharing process based on a <see cref="DynamicScenario"/>.
        /// </summary>
        /// <param name="dynamicScenario">
        /// Details of the device's callback endpoint, <see
        /// cref="Yoti.Auth.ShareUrl.Policy.DynamicPolicy"/> and extensions for the application
        /// </param>
        /// <returns><see cref="ShareUrlResult"/> containing a Sharing URL and Reference ID</returns>
        public ShareUrlResult CreateShareUrl(DynamicScenario dynamicScenario)
        {
            Task<ShareUrlResult> task = Task.Run(async () => await CreateShareUrlAsync(dynamicScenario).ConfigureAwait(false));

            return task.Result;
        }

        /// <summary>
        /// Asynchronously initiate a sharing process based on a <see cref="DynamicScenario"/>.
        /// </summary>
        /// <param name="dynamicScenario">
        /// Details of the device's callback endpoint, <see
        /// cref="Yoti.Auth.ShareUrl.Policy.DynamicPolicy"/> and extensions for the application
        /// </param>
        /// <returns><see cref="ShareUrlResult"/> containing a Sharing URL and Reference ID</returns>
        public async Task<ShareUrlResult> CreateShareUrlAsync(DynamicScenario dynamicScenario)
        {
            return await _yotiClientEngine.CreateShareURLAsync(_sdkId, _keyPair, ApiUri, dynamicScenario).ConfigureAwait(false);
        }

        internal void SetYotiApiUri()
        {
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("YOTI_API_URL")))
            {
                ApiUri = new Uri(Environment.GetEnvironmentVariable("YOTI_API_URL"));
            }
            else
            {
                ApiUri = new Uri(Constants.Api.DefaultYotiApiUrl);
            }
        }

        public YotiClient OverrideApiUri(Uri apiUri)
        {
            ApiUri = apiUri;

            return this;
        }
    }
}
